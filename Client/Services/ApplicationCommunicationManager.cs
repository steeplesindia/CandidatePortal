using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using System.Net.Http.Json;

namespace CandidatePortal.Client.Services
{
    public class ApplicationCommunicationManager : IApplicationCommunication
    {
        private readonly HttpClient _client;
        private readonly SpinnerService _spinnerService;

        public ApplicationCommunicationManager(HttpClient httpClient, SpinnerService spinnerService)
        {
            _client = httpClient;
            _spinnerService = spinnerService;
        }

        #region Communication
        //Add 
        public async Task<ResponseStatusData> AddCommunicationAsync(ApplicationCommunicationAddDTO item)
        {
            try
            {
                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/ApplicationCommunication", item);
                if(!Resp.IsSuccessStatusCode)
                {
                    throw new ApplicationException(Resp.ToString());
                }

                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> EditCommunicationAsync(ApplicationCommunicationAddDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/ApplicationCommunication", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ToString());
            }
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteCommunicationAsync(long Number)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/ApplicationCommunication/{Number}");
            if (!Resp.IsSuccessStatusCode)
            {
                throw new ApplicationException(Resp.ToString());
            }
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(long NApplicationNumber)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationCommunication/AppNumber/{NApplicationNumber}");
        }

        //Get All Details
        public async Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(long Id)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationCommunication/{Id}");
        }

        #endregion

        public async Task<ApplicationCommunicationDocumentAddDTO> UploadSingleFileWithRandomName(MultipartFormDataContent content)
        {
            _spinnerService.Show();
            await Task.Delay(1000);

            HttpResponseMessage postResult = await _client.PostAsync("v1.0/Upload/UploadSingleFileWithRandomName", content);
            return postResult.Content.ReadFromJsonAsync<ApplicationCommunicationDocumentAddDTO>().Result;
            _spinnerService.Hide();
        }

        public async Task<bool> DeleteImageFileAsync(ImageDeleteModel content)
        {
            HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Upload/DeleteFileAsync", content);
            return Resp.Content.ReadFromJsonAsync<bool>().Result;
        }

        public async Task<bool> DeleteAzureBlobFile(string FileName)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync("v1.0/Upload/DeleteAzureBlobFileNew/" + FileName);
            return Resp.Content.ReadFromJsonAsync<bool>().Result;
        }

        public async Task<ResponseStatusData> GetApplicationJobDetails(long ApplicationNumber)
        {
            try
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Jobs/JobDetailsApplication/ApplicationNumber/{ApplicationNumber}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> DownloadCommunication(string FileName)
        {
            try
            {
                return await _client.GetByteArrayAsync($"v1.0/Upload/DownloadCommunication/{FileName}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CandidatePortal.Client.Services
{
    public class ApplicationCheckListManager : IApplicationChecklist
    {
        private readonly HttpClient _client;
        private readonly SpinnerService _spinnerService;

        public ApplicationCheckListManager(HttpClient httpClient, SpinnerService spinnerService)
        {
            _client = httpClient;
            _spinnerService = spinnerService;
        }

        #region Check List
        //Add 
        public async Task<ResponseStatusData> Add(ApplicationChecklistAddDTO item)
        {
            try
            {
                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/ApplicationCheckList", item);
                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> Edit(ApplicationChecklistAddDTO item)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/ApplicationCheckList", item);
                return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Delete
        public async Task<ResponseStatusData> Delete(long Number)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/ApplicationCheckList/{Number}");
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetAppCommunicaionByAppsNumber(long NApplicationNumber)
        {
            try 
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationCheckList/ApplicationCheckList/ApplicationNumber/{NApplicationNumber}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get All Details
        public async Task<ResponseStatusData> GetAppCommunicaionByNumber(long Id)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationCheckList/ApplicationCheckList/Number/{Id}");
        }
        #endregion

        public async Task<ApplicationChecklistDocumentAddDTO> UploadSingleFileWithRandomNameCheckList(MultipartFormDataContent content)
        {
            _spinnerService.Show();
            await Task.Delay(1000);

            HttpResponseMessage postResult = await _client.PostAsync("v1.0/Upload/UploadSingleFileWithRandomNameCheckList", content);
            return postResult.Content.ReadFromJsonAsync<ApplicationChecklistDocumentAddDTO>().Result;
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

        public async Task<byte[]> Downloadchecklist(string FileName)
        {
            try
            {
                return await _client.GetByteArrayAsync($"v1.0/Upload/Downloadchecklist/{FileName}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

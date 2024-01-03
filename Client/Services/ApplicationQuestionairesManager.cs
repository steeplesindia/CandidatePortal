using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace CandidatePortal.Client.Services
{
    public class ApplicationQuestionairesManager : IApplicationQuestionaires
    {
        private readonly HttpClient _client;
        private readonly SpinnerService _spinnerService;

        public ApplicationQuestionairesManager(HttpClient httpClient, SpinnerService spinnerService)
        {
            _client = httpClient;
            _spinnerService = spinnerService;
        }

        #region Questionnaire

        //Edit(Update) 
        public async Task<ResponseStatusData> EditQuestionnaireAsync(ApplicationQuestionnaireDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/ApplicationQuestionnaires", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.ToString());
            }
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteQuestionnaireAsync(long Number)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/ApplicationQuestionnaires/{Number}");
            if (!Resp.IsSuccessStatusCode)
            {
                throw new ApplicationException(Resp.ToString());
            }
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetAppQuestionnaireByAppsNumberAsync(long NApplicationNumber)
        {
            try
            {
                var data = await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationQuestionnaires/AppNumber/{NApplicationNumber}");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get All Details
        public async Task<ResponseStatusData> GetAppQuestionnaireByNumberAsync(long Id)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/ApplicationQuestionnaires/{Id}");
        }
        #endregion
    }
}

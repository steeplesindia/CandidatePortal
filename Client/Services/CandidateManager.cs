using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace CandidatePortal.Client.Services
{
    public class CandidateManager : ICandidateRepo
    {
        private readonly HttpClient _client;
        private readonly SpinnerService _spinnerService;
        ResponseStatusData obj_ResponseStatusData = new ResponseStatusData();

        public CandidateManager(HttpClient httpClient, SpinnerService spinnerService)
        {
            _client = httpClient;
            _spinnerService = spinnerService;
        }

        #region Candidate
        //Add 
        public async Task<ResponseStatusData> AddCandidateAsync(CandidateAddEditDTO item)
        {
            try
            {
                //var content = JsonSerializer.Serialize(item);
                //var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                //var postResult = await _client.PostAsync("v1.0/Candidates", bodyContent);
                //var postContent = await postResult.Content.ReadAsStringAsync();
                //ResponseStatusData responseStatusData = new ResponseStatusData();
                //return responseStatusData;

                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Candidates", item);
                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edit(Update)
        public async Task<ResponseStatusData> EditCandidateAsync(CandidateAddEditDTO item)
        {

            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/Candidates", item);
                return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string SCandidateEmail)
        {
            try
            {
                CandidateDetails objModel = new CandidateDetails();
                HttpResponseMessage response = await _client.GetAsync($"v1.0/Candidates/GetCandidateDetailsByEmailIdNew/{SCandidateEmail}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (json != null && json != "" )
                    {
                        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                        //var person = JsonSerializer.Deserialize<Person>(json, options);
                        objModel = JsonSerializer.Deserialize<CandidateDetails>(json);
                    }
                }
                return objModel;
                //HttpResponseMessage Resp = await _client.GetAsync("v1.0/Candidates/GetCandidateDetailsByEmailIdNew/" + SCandidateEmail);
                //string responseString = await Resp.Content.ReadAsStringAsync();  
                //var employee = JsonSerializer.Deserialize<ResponseStatusData>(responseString);
                //return employee;
                //return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Candidates/GetCandidateDetailsByEmailIdNew/" + SCandidateEmail);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //Get By Id
        public async Task<ResponseStatusData> GetCandidateAsync(string EmailId)
        {
            var data = await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/email/{EmailId}");
            return data;
        }

        //Get All Details
        public async Task<ResponseStatusData> GetAllCandidatesAsync(CandidateParameters parameters)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/{parameters}");
        }
        #endregion

        #region Candidate Education
        //Add 
        public async Task<ResponseStatusData> AddCandidateEducationAsync(CandidateEducationAddEditDTO item)
        {
            HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Candidates/education", item);
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> EditCandidateEducationAsync(CandidateEducationAddEditDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/Candidates/education", item);
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteCandidateEducationAsync(long CandidateNumber, long EducationERPRecId)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/Candidates/education/{CandidateNumber}/{EducationERPRecId}");
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetCandidateEducationAsync(long CandidateNumber, long EducationERPRecId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/GetCandidateEducationAsync/{CandidateNumber}/{EducationERPRecId}");
        }

        #endregion

        #region Candidate Experience
        //Add 
        public async Task<ResponseStatusData> AddCandidateExperienceAsync(CandidateExperienceDTO item)
        {
            try
            {
                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Candidates/experience", item);
                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> EditCandidateExperienceAsync(CandidateExperienceDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/Candidates/experience", item);
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/Candidates/experience/{CandidateNumber}/{Employer}");
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/GetCandidateExperienceAsync/{CandidateNumber}/{Employer}");
        }
        #endregion

        #region Candidate Skill
        //Add 
        public async Task<ResponseStatusData> AddCandidateSkillAsync(CandidateSkillAddEditDTO item)
        {
            HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Candidates/skill", item);
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> EditCandidateSkillAsync(CandidateSkillAddEditDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/Candidates/skill", item);
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteCandidateSkillAsync(long CandidateNumber, long SkillERPRecId)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/Candidates/skill/{CandidateNumber}/{SkillERPRecId}");
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetCandidateSkillAsync(long CandidateNumber, long SkillERPRecId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/GetCandidateSkillAsync/{CandidateNumber}/{SkillERPRecId}");
        }
        #endregion

        #region Candidate Certificate
        //Add 
        public async Task<ResponseStatusData> AddCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item)
        {
            try
            {
                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Candidates/addcertificate", item);
                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edit(Update) 
        public async Task<ResponseStatusData> EditCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("v1.0/Candidates/certificate", item);
            return await response.Content.ReadFromJsonAsync<ResponseStatusData>();
        }

        //Delete
        public async Task<ResponseStatusData> DeleteCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/Candidates/certificate/{CandidateNumber}/{CertificateTypeERPRecId}");
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        //Get
        public async Task<ResponseStatusData> GetCandidateCertificateAsync(long CandidateNumber, long SkillERPRecId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Candidates/GetCandidateCertificateAsync/{CandidateNumber}/{SkillERPRecId}");
        }
        #endregion

        #region Get Master
        //public async Task<ResponseStatusData> UploadCadidatePhotoResume(MultipartFormDataContent? Content)
        //{
        //    HttpResponseMessage Resp = await _client.PostAsync("v1.0/Upload/UploadSingleFile/", Content);
        //    return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        //}

        public async Task<ResponseStatusData> UploadCadidateResume(MultipartFormDataContent Content)
        {
            HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Upload/UploadSingleFile", Content);
            return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
        }

        public async Task<string> UploadSingleFile(MultipartFormDataContent content)
        {
            _spinnerService.Show();
            await Task.Delay(1000);

            var postResult = await _client.PostAsync("v1.0/Upload/UploadSingleFile", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
                _spinnerService.Hide();
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return imgUrl;
                _spinnerService.Hide();
            }
        }

        public async Task<bool> DeleteImageFileAsync(ImageDeleteModel content)
        {
            HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Upload/DeleteFileAsync", content);
            return Resp.Content.ReadFromJsonAsync<bool>().Result;
        }

        public async Task<bool> DeleteAzureBlobFile(string fileName, string folderName)
        {
            HttpResponseMessage Resp = await _client.DeleteAsync($"v1.0/Upload/DeleteAzureBlobFile/{fileName}/{folderName}");
            return Resp.Content.ReadFromJsonAsync<bool>().Result;
        }
        
        public async Task<ResponseStatusData> GetEducationLevelAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/educationlevel");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseStatusData> GetCountriesAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/countries");
        }

        public async Task<ResponseStatusData> GetStatesByCountryIdAsync(long? countryId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/country/" + countryId + "/states");
        }

        public async Task<ResponseStatusData> GetCitiesByCountryIdAsync(long? countryId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/country/" + countryId + "/cities");
        }

        public async Task<ResponseStatusData> GetCitiesByStateIdAsync(long? stateId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/state/" + stateId + "/cities");
        }

        public async Task<ResponseStatusData> GetSkillBySkillIdAsync(long? skillId)
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/GetSkillBySkillIdAsync/" + skillId);
        }

        //Upload Image And Resume
        public async Task<FileNameListModel> UploadImage(MultipartFormDataContent content)
        {
            try
            {
                _spinnerService.Show();

                HttpResponseMessage postResult = await _client.PostAsync("v1.0/Upload/UploadCandidatePhoto/", content);
                return postResult.Content.ReadFromJsonAsync<FileNameListModel>().Result;
                _spinnerService.Hide();

                //await Task.Delay(1000);

                //var postResult = await _client.PostAsync("v1.0/Upload/UploadImage/", content);
                //var postContent = await postResult.Content.ReadAsStringAsync();
                //if (!postResult.IsSuccessStatusCode)
                //{
                //    _spinnerService.Hide();
                //    throw new ApplicationException(postContent);
                //}
                //else
                //{
                //    _spinnerService.Hide();
                //    var imgUrl = Path.Combine("", postContent);
                //    return imgUrl;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FileNameListModel> UploadResume(MultipartFormDataContent content)
        {

            HttpResponseMessage postResult = await _client.PostAsync("v1.0/Upload/UploadCandidateResume/", content);
            return postResult.Content.ReadFromJsonAsync<FileNameListModel>().Result;
            _spinnerService.Hide();


            //var postResult = await _client.PostAsync("v1.0/Upload/UploadResume/", content);
            //var postContent = await postResult.Content.ReadAsStringAsync();
            //if (!postResult.IsSuccessStatusCode)
            //{
            //    throw new ApplicationException(postContent);
            //}
            //else
            //{
            //    var imgUrl = Path.Combine("", postContent);
            //    return imgUrl;
            //}
        }
        //==============

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("v1.0/Upload/UploadImage/", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return imgUrl;
            }
        }


        public async Task<ResponseStatusData> GetMasterListAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/GetMasterListAsync");
        }

        public async Task<ResponseStatusData> GetPersonalTitleAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/personaltitles");
        }

        public async Task<ResponseStatusData> GetEducationAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/educations");
        }

        public async Task<ResponseStatusData> GetSkillsAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/skills");
        }

        public async Task<ResponseStatusData> GetSkillLevelsAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/skilllevels");
        }


        public async Task<ResponseStatusData> GetCertificateTypesAsync()
        {
            return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Masters/certificatetypes");
        }

        public async Task<bool> CheckCandidateEmailAvailability(string EmailId)
        {
            return await _client.GetFromJsonAsync<bool>("v1.0/Candidates/CheckCandidateEmailAvailability/" + EmailId);
        }
        #endregion
    }
}
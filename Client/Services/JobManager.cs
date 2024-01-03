using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.Net.Http.Json;
using System.Text.Json;


namespace CandidatePortal.Client.Services
{
    public class JobManager : IJobRepo
    {
        private readonly HttpClient _client;
        private readonly HttpClient _clientPublic;
        private readonly IHttpClientFactory clientFactoryPublic;

        private readonly JsonSerializerOptions _options;

        public JobManager(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            clientFactoryPublic = httpClientFactory;
            _clientPublic = clientFactoryPublic.CreateClient("CandidatePortal.ServerAPI.Public");
            _client = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<PagingResponse<JobDTO>> SearchJob(JobSearchParameter parameters)
        {
            var response = await _client.GetAsync($"v1.0/Jobs/Search/{parameters.SearchTerm}/{parameters.sLocationSearch}/{parameters.PageNumber}/{parameters.PageSize}");// need query base parameter, Ashwin
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content); // need message here rather than exception, Ashwin
            }
            var pagingResponse = new PagingResponse<JobDTO>
            {
                Items = JsonSerializer.Deserialize<List<JobDTO>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingResponse;
        }

        //public async Task<PagedList<JobDTO>> SearchJob(JobSearchParameter parameters)
        //{
        //    try
        //    {
        //        return await _client.GetFromJsonAsync<PagedList<JobDTO>>($"v1.0/Jobs/JobService/{parameters.SearchTerm}/{parameters.PageNumber}/{parameters.PageSize}");

        //        //return await _client.GetFromJsonAsync<PagedList<ResponseStatusData>>($"v1.0/Jobs/JobService/{parameters.SearchTerm}/{parameters.PageNumber}/{parameters.PageSize}");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<PagingResponse<JobDTO>> GetDefaultJobList(JobSearchParameter parameters)
        {
            try
            {
                var response = await _clientPublic.GetAsync($"v1.0/Jobs/DefaultJobList/{parameters.PageNumber}/{parameters.PageSize}"); // need query base parameter, Ashwin
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);// need message here rather than exception, Ashwin
                }
                var pagingResponse = new PagingResponse<JobDTO>
                {
                    Items = JsonSerializer.Deserialize<List<JobDTO>>(content, _options),
                    MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
                };
                return pagingResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseStatusData> ApplyJobApplication(ApplicationDTO item)
        {
            try
            {
                HttpResponseMessage Resp = await _client.PostAsJsonAsync("v1.0/Applications", item);
                return Resp.Content.ReadFromJsonAsync<ResponseStatusData>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public async Task<ResponseStatusData> GetJobDetailsById(long JobERPRecId, string SCandidateEmail)
        {
            try
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Jobs/JobDetails/" + JobERPRecId + "/" + SCandidateEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseStatusData> GetJobDetailsByJobId(long JobERPRecId)
        {
            try
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Jobs/GetJobDetailsByJobId/" + JobERPRecId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<ResponseStatusData> GetAppliedJobByStatus(Int16 ApplicationStatus, long? nCandidateId)
        {
            try
            {
                return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Jobs/JobStatus/" + ApplicationStatus + "/" + nCandidateId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<ResponseStatusData> GetCandidateDetailsByEmailIdNew(string SCandidateEmail)
        //{
        //    return await _client.GetFromJsonAsync<ResponseStatusData>("v1.0/Candidates/GetCandidateDetailsByEmailIdNew/" + SCandidateEmail);
        //}

        public async Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string SCandidateEmail)
        {
            try
            {
                CandidateDetails objModel = new CandidateDetails();
                HttpResponseMessage response = await _client.GetAsync($"v1.0/Candidates/GetCandidateDetailsByEmailIdNew/{SCandidateEmail}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    if (json != null && json != "")
                    {
                        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                        //var person = JsonSerializer.Deserialize<Person>(json, options);
                        objModel = JsonSerializer.Deserialize<CandidateDetails>(json);
                    }
                }
                return objModel;
             }
            catch (Exception ex)
            {

                throw;
            }
        }

        ////Get All Details
        //public async Task<ResponseStatusData> GetAllCandidatesAsync(CandidateParameters parameters)
        //{
        //    return await _client.GetFromJsonAsync<ResponseStatusData>($"v1.0/Jobs/{parameters}");
        //}
    }
}

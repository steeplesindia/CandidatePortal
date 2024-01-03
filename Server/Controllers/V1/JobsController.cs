using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CandidatePortal.Server.Controllers.V1
{
    [Route("api/v{:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    //[Authorize]

    public class JobsController : ControllerBase
    {
        private readonly IJobService _repo;
        private readonly ICandidateService _repoCand;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;

        public JobsController(IJobService repo, ICandidateService repoCand, IReturnResponseService returnResponse, ILoggerService logger)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
            _repoCand = repoCand;
        }

        [HttpGet("Search/{SearchTerm}/{sLocationSearch}/{PageNumber}/{PageSize}")]
        public async Task<IActionResult> JobService(string SearchTerm, string sLocationSearch, int PageNumber, int PageSize)
        {
            //var result = await _repo.SearchJob(SearchTerm, PageNumber, PageSize);
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            //return resp.ReturnResponseData(result);

            var products = await _repo.SearchJob(SearchTerm, sLocationSearch, PageNumber, PageSize);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(products);
        }

        [HttpGet("Job/Date/fromDate/toDate/nCandidateId")]
        public async Task<IActionResult> GetAppliedJobByDateRange(DateTime fromDate, DateTime toDate, long nCandidateId)
        {
            var result = await _repo.GetAppliedJobByDateRange(fromDate, toDate, nCandidateId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("JobStatus/{ApplicationStatus}/{nCandidateId}")]
        public async Task<IActionResult> GetAppliedJobByStatus(Int16 ApplicationStatus, long nCandidateId)
        {
            var result = await _repo.GetAppliedJobByStatus(ApplicationStatus, nCandidateId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("JobDetails/{JobERPRecId}/{SCandidateEmail}")]
        public async Task<IActionResult> GetJobDetailsById(long JobERPRecId, string SCandidateEmail)
        {
            if (!await _repoCand.CheckCandidateEmailAvailability(SCandidateEmail))
            {
                _logger.LogError($"Candidate with email: {SCandidateEmail}, has been not found in db.");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 19, ErrorStatus.Warning, 400, false));
            }

            var result = await _repo.GetJobDetailsById(JobERPRecId, SCandidateEmail);
            return resp.ReturnResponseData(result);
        }

        [AllowAnonymous]
        [HttpGet("GetJobDetailsByJobId/{JobERPRecId}")]
        public async Task<IActionResult> GetJobDetailsByJobId(long JobERPRecId)
        {
            var result = await _repo.GetJobDetailsByJobId(JobERPRecId);
            return resp.ReturnResponseData(result);
        }

        [AllowAnonymous]
        [HttpGet("DefaultJobList/{PageNumber}/{PageSize}")]
        public async Task<IActionResult> DefaultJobList(int PageNumber, int PageSize)
        {
            try
            {
                var products = await _repo.DefaultJobList(PageNumber, PageSize);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
                return Ok(products);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("JobDetailsApplication/ApplicationNumber/{ApplicationNumber}")]
        public async Task<IActionResult> GetApplicationJobDetails(long ApplicationNumber)
        {
            var result = await _repo.GetApplicationJobDetails(ApplicationNumber);
            return resp.ReturnResponseData(result);
        }

        [HttpPost("UpdateJobUrl/{joberpid}")]
        public async Task<IActionResult> UpdateJobUrl(long joberpid)
        {
            var result = await _repo.UpdateJobUrl(joberpid);
            return resp.ReturnResponseData(result);
        }
    }
}

using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Server.Controllers.V1
{
    [Route("api/v{:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    //[Authorize]

    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _repo;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
       // private readonly IUrlHelper _urlHelper;

        public ApplicationsController(IApplicationService repo, IReturnResponseService returnResponse, ILoggerService logger, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
           // _urlHelper = urlHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            if (!_repo.CheckDuplicateApplicationAsync(item.CandidateNumber, item.JobERPRecId))
            {
                _logger.LogError($"Candidate with same job has been found in system.");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 18, ErrorStatus.Warning, 400, false));
            }

            //var request = _httpContextAccessor.HttpContext.Request;

            //var url = new Uri(new Uri($"{request.Scheme}://{request.Host.Value}").ToString()).ToString();


            //var request = _urlHelper.ActionContext.HttpContext.Request;
            //var absoluteUri = string.Concat(
            //            request.Scheme,
            //            "://",
            //            request.Host.ToUriComponent(),
            //            request.PathBase.ToUriComponent(),
            //            request.Path.ToUriComponent(),
            //            request.QueryString.ToUriComponent());
            //string sUrl = _httpContextAccessor.HttpContext.Request.Path;
            var result = await _repo.ApplyJobAsync(item);

            return resp.ReturnResponseData(result);
        }

    }
}

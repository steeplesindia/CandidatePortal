using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Server.Controllers.V1
{
    [Route("api/v{:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    //[Authorize]

    public class ApplicationQuestionnairesController : ControllerBase
    {
        private readonly IApplicationQuestionnairesService _repo;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;

        public ApplicationQuestionnairesController(IApplicationQuestionnairesService repo, IReturnResponseService returnResponse, ILoggerService logger)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ApplicationQuestionnaireDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Communication Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Communication Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            var result = await _repo.EditAppsQuestionnaireAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long Number)
        {

            _logger.LogInfo($"Returned Communication with id: {Number}");

            var result = await _repo.DeleteAppQuestionnaireAsync(Number);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("AppNumber/{NApplicationNumber}")]
        public async Task<IActionResult> GetAppQuestionnaireByAppsNumberAsync(long NApplicationNumber)
        {
            var result = await _repo.GetAppQuestionnaireByAppsNumberAsync(NApplicationNumber);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAppQuestionnaireByNumberAsync(long Id)
        {
            var result = await _repo.GetAppQuestionnaireByNumberAsync(Id);
            return resp.ReturnResponseData(result);
        }
    }
}

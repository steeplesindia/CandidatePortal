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

    public class ApplicationChecklistController : ControllerBase
    {
        private readonly IApplicationCheckListService _repo;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;

        public ApplicationChecklistController(IApplicationCheckListService repo, IReturnResponseService returnResponse, ILoggerService logger)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationChecklistAddDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Checklist Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Checklist Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }
            var result = await _repo.AddAppsCommunicationAsync(item);
            return resp.ReturnResponseData(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ApplicationChecklistAddDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Checklist Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Checklist Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            var result = await _repo.EditAppsCommunicationAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Number)
        {

            _logger.LogInfo($"Returned Checklist with email: {Number}");

            var result = await _repo.DeleteAppCommunicaionAsync(Number);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("ApplicationChecklist/ApplicationNumber/{NApplicationNumber}")]
        public async Task<IActionResult> GetAppCommunicaionByAppsNumberAsync(long NApplicationNumber)
        {
           
            var result = await _repo.GetAppCommunicaionByAppsNumberAsync(NApplicationNumber);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("ApplicationChecklist/Number/{Id}")]
        public async Task<IActionResult> GetAppCommunicaionByNumberAsync(int Id)
        { 
            var result = await _repo.GetAppCommunicaionByNumberAsync(Id);
            return resp.ReturnResponseData(result);
        }
    }
}

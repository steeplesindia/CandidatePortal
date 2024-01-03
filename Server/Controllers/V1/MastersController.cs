using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Server.Controllers.V1
{
    [Route("api/v{:apiVersion}/[controller]")]
    [ApiController, ApiVersion("1.0")]
    //[Authorize]

    public class MastersController : ControllerBase
    {
        private readonly IMasterService _repo;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;

        public MastersController(IMasterService repo, IReturnResponseService returnResponse, ILoggerService logger)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var result = await _repo.GetCountriesAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStatesAsync()
        {
            var result = await _repo.GetStatesAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("country/{countryId}/states")]
        public async Task<IActionResult> GetStatesByCountryIdAsync(long countryId)
        {
            var result = await _repo.GetStatesByCountryIdAsync(countryId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("country/{countryId}/cities")]
        public async Task<IActionResult> GetCitiesByCountryIdAsync(long countryId)
        {
            var result = await _repo.GetCitiesByCountryIdAsync(countryId);
            return resp.ReturnResponseData(result);
        }


        [HttpGet("GetMasterListAsync")]
        public async Task<IActionResult> GetMasterListAsync()
        {
            var result = await _repo.GetMasterListAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var result = await _repo.GetCitiesAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("state/{stateId}/cities")]
        public async Task<IActionResult> GetCitiesByStateIdAsync(long stateId)
        {
            var result = await _repo.GetCitiesByStateIdAsync(stateId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetSkillBySkillIdAsync/{skillId}")]
        public async Task<IActionResult> GetSkillBySkillIdAsync(long skillId)
        {
            var result = await _repo.GetSkillBySkillIdAsync(skillId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("educationlevel")]
        public async Task<IActionResult> GetEducationLevelAsync()
        {
            var result = await _repo.GetEducationLevelAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("educations")]
        public async Task<IActionResult> GetEducationAsync()
        {
            var result = await _repo.GetEducationsAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("personaltitles")]
        public async Task<IActionResult> GetPersonalTitleAsync()
        {
            var result = await _repo.GetPersonalTitlesAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("skills")]
        public async Task<IActionResult> GetSkillsAsync()
        {
            var result = await _repo.GetSkillsAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("skilllevels")]
        public async Task<IActionResult> GetSkillLevelsAsync()
        {
            var result = await _repo.GetSkillLevelsAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpGet("certificatetypes")]
        public async Task<IActionResult> GetCertificateTypesAsync()
        {
            var result = await _repo.GetCertificateTypesAsync();
            return resp.ReturnResponseData(result);
        }

        [HttpPost("country")]
        public async Task<IActionResult> AddCountryAsync([FromBody] CountryDTO item)
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

            if (_repo.GetCountryAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddCountryAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("state")]
        public async Task<IActionResult> AddStateAsync([FromBody] StateDTO item)
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

            if (_repo.GetStatesAsync(item.Code, item.CountryERPRecId).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddStateAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("city")]
        public async Task<IActionResult> AddCityAsync([FromBody] CityDTO item)
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

            if (_repo.GetCityAsync(item.Code, item.StateERPRecId).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddCityAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("education")]
        public async Task<IActionResult> AddEducationAsync([FromBody] EducationDTO item)
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

            if (_repo.GetEducationAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddEducationAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("personaltitle")]
        public async Task<IActionResult> AddPersonalTitleAsync([FromBody] PersonalTitleDTO item)
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

            if (_repo.GetPersonalTitleAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddPersonalTitleAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("skill")]
        public async Task<IActionResult> AddSkillAsync([FromBody] SkillDTO item)
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

            if (_repo.GetSkillAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddSkillAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("skilllevel")]
        public async Task<IActionResult> AddSkillLevelAsync([FromBody] SkillLevelDTO item)
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

            if (_repo.GetSkillLevelAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddSkillLevelAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPost("certificatetype")]
        public async Task<IActionResult> AddCertificateTypeAsync([FromBody] CertificateTypeDTO item)
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

            if (_repo.GetCertificateTypeAsync(item.Code).Result is not null)
            {
                _logger.LogError($"Record found with same name");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 15, ErrorStatus.Warning, 204, false));
            }

            var result = await _repo.AddCertificateTypeAsync(item);

            return resp.ReturnResponseData(result);
        }



    }
}

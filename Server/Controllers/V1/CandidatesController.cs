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

    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _repo;
        private readonly IReturnResponseService _returnResponse;
        ReturnResponses resp = new ReturnResponses();
        private ILoggerService _logger;

        public CandidatesController(ICandidateService repo, IReturnResponseService returnResponse, ILoggerService logger)
        {
            _repo = repo;
            _returnResponse = returnResponse;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CandidateAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            if (await _repo.CheckCandidateEmailAvailability(item.Email))
            {
                _logger.LogError($"Candidate with email: {item.Email}, has been found in db.");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 12, ErrorStatus.Warning, 400, false));
            }
            
            var result = await _repo.AddCandidateAsync(item);
            
            return resp.ReturnResponseData(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CandidateAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            var result = await _repo.EditCandidateAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpGet("email/{EmailId}")]
        public async Task<IActionResult> GetCandidateByEmail(string EmailId)
        {
            if (!await _repo.CheckCandidateEmailAvailability(EmailId))
            {
                _logger.LogError($"Candidate with email: {EmailId}, hasn't been found in db.");
                return NotFound();
                //return resp.ReturnResponseData(_returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false));
            }
            else
            {
                _logger.LogInfo($"Returned Candidate with email: {EmailId}");

                var result = await _repo.GetCandidateAsync(EmailId);
                return resp.ReturnResponseData(result);
            }
        }

        [HttpGet("GetCandidateDetailsByEmailId/email/{EmailId}")]
        public async Task<IActionResult> GetCandidateDetailsByEmailId(string EmailId)
        {
            var result = await _repo.GetCandidateAsync(EmailId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("CheckCandidateEmailAvailability/{EmailId}")]
        public async Task<bool> CheckCandidateEmailAvailability(string EmailId)
        {
            return await _repo.CheckCandidateEmailAvailability(EmailId);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCandidateById(long Id)
        {
            var result = await _repo.GetCandidateAsync(Id);
            return resp.ReturnResponseData(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates([FromQuery] CandidateParameters parameters)
        {
            var result = await _repo.GetAllCandidatesAsync(parameters);
            return resp.ReturnResponseData(result);
        }

        [HttpPost("addcertificate")]
        public async Task<IActionResult> AddCandidateCertificate([FromBody] CandidateCertificateTypeAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Certificate Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Certificate Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            // Pending, Check duplicate certificate if neccessary

            var result = await _repo.AddCandidateCertificateAsync(item);
            return resp.ReturnResponseData(result);
        }

        [HttpPost("education")]
        public async Task<IActionResult> AddCandidateEducation([FromBody] CandidateEducationAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Education Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Education Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            if (!_repo.CheckCandidateEducationAvailability(item.CandidateNumber, item.EducationERPRecId))
            {
                _logger.LogError($"Candidate with same education type has been found in db.");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 14, ErrorStatus.Warning, 400, false));
            }

            var result = await _repo.AddCandidateEducationAsync(item);
            return resp.ReturnResponseData(result);
        }

        [HttpPost("skill")]
        public async Task<IActionResult> AddCandidateSkill([FromBody] CandidateSkillAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Skill Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Skill Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            if (!_repo.CheckCandidateSkillAvailability(item.CandidateNumber, Convert.ToInt64(item.SkillERPRecId)))
            {
                _logger.LogError($"Candidate with same skill has been found in db.");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 14, ErrorStatus.Warning, 400, false));
            }

            var result = await _repo.AddCandidateSkillAsync(item);
            return resp.ReturnResponseData(result);
        }

        [HttpPost("experience")]
        public async Task<IActionResult> AddCandidateExperience([FromBody] CandidateExperienceDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Experience Model Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Experience Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            // Pending, Check duplicate Experience if neccessary

            var result = await _repo.AddCandidateExperienceAsync(item);
            return resp.ReturnResponseData(result);
        }

        [HttpPut("certificate")]
        public async Task<IActionResult> EditCandidateCertificate([FromBody] CandidateCertificateTypeAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Certificate Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Certificate Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            var result = await _repo.EditCandidateCertificateAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPut("education")]
        public async Task<IActionResult> EditCandidateEducation([FromBody] CandidateEducationAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Education Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Education Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            //if (!_repo.CheckCandidateEducationAvailability(item.CandidateNumber, item.EducationERPRecId, item.CandidateEduId))
            //{
            //    _logger.LogError($"Candidate with same education type has been found in db.");
            //    return BadRequest(_returnResponse.ReturnResponse(null, 0, 14, ErrorStatus.Warning, 400, false));
            //}

            var result = await _repo.EditCandidateEducationAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPut("skill")]
        public async Task<IActionResult> EditCandidateSkill([FromBody] CandidateSkillAddEditDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Skill Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Skill Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            //if (!_repo.CheckCandidateSkillAvailability(item.CandidateNumber, item.SkillERPRecId, item.CandidateSkillId))
            //{
            //    _logger.LogError($"Candidate with same skill has been found in db.");
            //    return BadRequest(_returnResponse.ReturnResponse(null, 0, 14, ErrorStatus.Warning, 400, false));
            //}

            var result = await _repo.EditCandidateSkillAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpPut("experience")]
        public async Task<IActionResult> EditCandidateExperience([FromBody] CandidateExperienceDTO item)
        {
            if (item == null)
            {
                _logger.LogError($"Candidte Experience Is Null");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 10, ErrorStatus.Error, 400, false));
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Candidte Experience Model Invalid");
                return BadRequest(_returnResponse.ReturnResponse(null, 0, 11, ErrorStatus.Error, 400, false));
            }

            var result = await _repo.EditCandidateExperienceAsync(item);

            return resp.ReturnResponseData(result);
        }

        [HttpDelete("certificate/{CandidateNumber}/{CertificateTypeERPRecId}")]
        public async Task<IActionResult> DeleteCertificate(long CandidateNumber, long CertificateTypeERPRecId)
        {
            var result = await _repo.DeleteCandidateCertificateAsync(CandidateNumber, CertificateTypeERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpDelete("education/{CandidateNumber}/{EducationERPRecId}")]
        public async Task<IActionResult> DeleteEducation(long CandidateNumber, long EducationERPRecId)
        {
            var result = await _repo.DeleteCandidateEducationAsync(CandidateNumber, EducationERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpDelete("experience/{CandidateNumber}/{Employer}")]
        public async Task<IActionResult> DeleteExperience(long CandidateNumber, string Employer)
        {
            var result = await _repo.DeleteCandidateExperienceAsync(CandidateNumber, Employer);
            return resp.ReturnResponseData(result);
        }

        [HttpDelete("skill/{CandidateNumber}/{SkillERPRecId}")]
        public async Task<IActionResult> DeleteSkill(long CandidateNumber, long SkillERPRecId)
        {
            var result = await _repo.DeleteCandidateSkillAsync(CandidateNumber, SkillERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetCandidateExperienceAsync/{CandidateNumber}/{Employer}")]
        public async Task<IActionResult> GetCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            var result = await _repo.GetCandidateExperienceAsync(CandidateNumber, Employer);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetCandidateEducationAsync/{CandidateNumber}/{EducationERPRecId}")]
        public async Task<IActionResult> GetCandidateEducationAsync(long CandidateNumber, long EducationERPRecId)
        {
            var result = await _repo.GetCandidateEducationAsync(CandidateNumber, EducationERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetCandidateSkillAsync/{CandidateNumber}/{SkillERPRecId}")]
        public async Task<IActionResult> GetCandidateSkillAsync(long CandidateNumber, long SkillERPRecId)
        {
            var result = await _repo.GetCandidateSkillAsync(CandidateNumber, SkillERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetCandidateCertificateAsync/{CandidateNumber}/{CertificateTypeERPRecId}")]
        public async Task<IActionResult> GetCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId)
        {
            var result = await _repo.GetCandidateCertificateAsync(CandidateNumber, CertificateTypeERPRecId);
            return resp.ReturnResponseData(result);
        }

        [HttpGet("GetCandidateDetailsByEmailIdNew/{EmailId}")]
        public async Task<IActionResult> GetCandidateDetailsByEmailIdNew(string EmailId)
        {
            var result = await _repo.GetCandidateDetailsByEmailIdNew(EmailId);
            return Ok(result);
           
        }
    }
}

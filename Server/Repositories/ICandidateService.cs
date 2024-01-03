using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Server.Repositories
{
    public interface ICandidateService
    {
        Task<ResponseStatusData> AddCandidateAsync(CandidateAddEditDTO item);
        Task<ResponseStatusData> EditCandidateAsync(CandidateAddEditDTO item);

        
        Task<ResponseStatusData> AddCandidateEducationAsync(CandidateEducationAddEditDTO item);
        Task<ResponseStatusData> EditCandidateEducationAsync(CandidateEducationAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateEducationAsync(long CandidateNumber, long EducationERPRecId);

        
        Task<ResponseStatusData> AddCandidateExperienceAsync(CandidateExperienceDTO item);
        Task<ResponseStatusData> EditCandidateExperienceAsync(CandidateExperienceDTO item);
        Task<ResponseStatusData> DeleteCandidateExperienceAsync(long CandidateNumber, string Employer);

        
        Task<ResponseStatusData> AddCandidateSkillAsync(CandidateSkillAddEditDTO item);
        Task<ResponseStatusData> EditCandidateSkillAsync(CandidateSkillAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateSkillAsync(long CandidateNumber, long SkillERPRecId);


        Task<ResponseStatusData> AddCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item);
        Task<ResponseStatusData> EditCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId);

        Task<ResponseStatusData> GetCandidateAsync(string EmailId);

        Task<ResponseStatusData> GetCandidateAsync(long CandidateId);

        Task<ResponseStatusData> GetAllCandidatesAsync(CandidateParameters dto);

        Task<bool> CheckCandidateEmailAvailability(string EmailId);

        bool CheckCandidateEducationAvailability(long CandidateNumber, long? EducationERPRecId, long CandidateEduId = 0);

        bool CheckCandidateSkillAvailability(long CandidateNumber, long SkillERPRecId, long CandidateSkillId = 0);

        Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string EmailId);

        Task<ResponseStatusData> GetCandidateEducationAsync(long CandidateNumber, long EducationERPRecId);
        Task<ResponseStatusData> GetCandidateExperienceAsync(long CandidateNumber, string Employer);
        Task<ResponseStatusData> GetCandidateSkillAsync(long CandidateNumber, long SkillERPRecId);
        Task<ResponseStatusData> GetCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId);
    }
}

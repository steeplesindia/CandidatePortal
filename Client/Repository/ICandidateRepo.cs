using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Client.Repository
{
    public interface ICandidateRepo
    {
        #region Candidate
        Task<ResponseStatusData> AddCandidateAsync(CandidateAddEditDTO item);
        Task<ResponseStatusData> EditCandidateAsync(CandidateAddEditDTO item);
        Task<ResponseStatusData> GetCandidateAsync(string EmailId);
        Task<ResponseStatusData> GetAllCandidatesAsync(CandidateParameters dto);
        #endregion

        #region Candidate Education
        Task<ResponseStatusData> AddCandidateEducationAsync(CandidateEducationAddEditDTO item);
        Task<ResponseStatusData> EditCandidateEducationAsync(CandidateEducationAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateEducationAsync(long CandidateNumber, long EducationERPRecId);
        Task<ResponseStatusData> GetCandidateEducationAsync(long CandidateNumber, long EducationERPRecId);
        #endregion

        #region Experience
        Task<ResponseStatusData> AddCandidateExperienceAsync(CandidateExperienceDTO item);
        Task<ResponseStatusData> EditCandidateExperienceAsync(CandidateExperienceDTO item);
        Task<ResponseStatusData> DeleteCandidateExperienceAsync(long CandidateNumber, string Employer);
        Task<ResponseStatusData> GetCandidateExperienceAsync(long CandidateNumber, string Employer);
        #endregion

        #region Skill
        Task<ResponseStatusData> AddCandidateSkillAsync(CandidateSkillAddEditDTO item);
        Task<ResponseStatusData> EditCandidateSkillAsync(CandidateSkillAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateSkillAsync(long CandidateNumber, long SkillERPRecId);
        Task<ResponseStatusData> GetCandidateSkillAsync(long CandidateNumber, long SkillERPRecId);
        #endregion

        #region Certificate
        Task<ResponseStatusData> AddCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item);
        Task<ResponseStatusData> EditCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item);
        Task<ResponseStatusData> DeleteCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId);
        Task<ResponseStatusData> GetCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId);
        #endregion

        #region Master
        //Task<ResponseStatusData> UploadCadidatePhotoResume(MultipartFormDataContent? Content);
        Task<ResponseStatusData> UploadCadidateResume(MultipartFormDataContent Content);

        Task<ResponseStatusData> GetCountriesAsync();

        Task<ResponseStatusData> GetStatesByCountryIdAsync(long? countryId);

        Task<ResponseStatusData> GetCitiesByStateIdAsync(long? stateId);

        Task<ResponseStatusData> GetCitiesByCountryIdAsync(long? countryId);

        Task<ResponseStatusData> GetSkillBySkillIdAsync(long? skillId);

        Task<FileNameListModel> UploadImage(MultipartFormDataContent content);
        Task<FileNameListModel> UploadResume(MultipartFormDataContent content);

        Task<string> UploadSingleFile(MultipartFormDataContent content);
        Task<string> UploadProductImage(MultipartFormDataContent content);

        Task<bool> DeleteImageFileAsync(ImageDeleteModel obj_ImageModel);
        Task<bool> DeleteAzureBlobFile(string FileName, string foldername);
        
        Task<ResponseStatusData> GetMasterListAsync();

        Task<ResponseStatusData> GetPersonalTitleAsync();
        Task<ResponseStatusData> GetEducationAsync();
        Task<ResponseStatusData> GetEducationLevelAsync();

        Task<ResponseStatusData> GetSkillsAsync();
        Task<ResponseStatusData> GetCertificateTypesAsync();
        Task<ResponseStatusData> GetSkillLevelsAsync();
        Task<bool> CheckCandidateEmailAvailability(string EmailId);
        Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string EmailId);
        #endregion
    }
}

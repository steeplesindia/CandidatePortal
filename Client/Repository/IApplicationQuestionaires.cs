using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Client.Repository
{
    public interface IApplicationQuestionaires
    {
        #region Candidate
        Task<ResponseStatusData> EditQuestionnaireAsync(ApplicationQuestionnaireDTO item);
        Task<ResponseStatusData> DeleteQuestionnaireAsync(long Number);
        Task<ResponseStatusData> GetAppQuestionnaireByAppsNumberAsync(long NApplicationNumber);
        Task<ResponseStatusData> GetAppQuestionnaireByNumberAsync(long Id);
        //Task<ResponseStatusData> GetApplicationJobDetails(long ApplicationNumber);
        #endregion
    }
}

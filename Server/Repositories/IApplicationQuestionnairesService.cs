using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Server.Repositories
{
    public interface IApplicationQuestionnairesService
    {
       //Task<ResponseStatusData> AddAppsCommunicationAsync(ApplicationCommunicationAddDTO item);
       Task<ResponseStatusData> EditAppsQuestionnaireAsync(ApplicationQuestionnaireDTO item);
       Task<ResponseStatusData> DeleteAppQuestionnaireAsync(long Number);
       Task<ResponseStatusData> GetAppQuestionnaireByAppsNumberAsync(long NApplicationNumber);
        Task<ResponseStatusData> GetAppQuestionnaireByNumberAsync(long Number);


    }
}

using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Server.Repositories
{
    public interface IApplicationCheckListService
    {
       Task<ResponseStatusData> AddAppsCommunicationAsync(ApplicationChecklistAddDTO item);
       Task<ResponseStatusData> EditAppsCommunicationAsync(ApplicationChecklistAddDTO item);
       Task<ResponseStatusData> DeleteAppCommunicaionAsync(long Number);
       Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(long NApplicationNumber);
       Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(int Number);
    }
}

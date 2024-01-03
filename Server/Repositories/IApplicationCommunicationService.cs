using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Server.Repositories
{
    public interface IApplicationCommunicationService
    {
       Task<ResponseStatusData> AddAppsCommunicationAsync(ApplicationCommunicationAddDTO item);
       Task<ResponseStatusData> EditAppsCommunicationAsync(ApplicationCommunicationAddDTO item);
       Task<ResponseStatusData> DeleteAppCommunicaionAsync(long Number);
       Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(int NApplicationNumber);
        Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(int Number);


    }
}

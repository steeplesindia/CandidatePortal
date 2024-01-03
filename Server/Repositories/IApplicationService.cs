using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Server.Repositories
{
    public interface IApplicationService
    {
        Task<ResponseStatusData> ApplyJobAsync(ApplicationDTO item);

        bool CheckDuplicateApplicationAsync(long CandidateNumber, long JobERPRecId);
    }
}

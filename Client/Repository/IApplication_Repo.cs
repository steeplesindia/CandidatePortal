using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Client.Repository
{
    public interface IApplication_Repo
    {
        Task<ResponseStatusData> ApplyJobApplication(ApplicationDTO dto);

    }
}

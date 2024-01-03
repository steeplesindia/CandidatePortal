using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using CandidatePortal.Shared.Helpers;

namespace CandidatePortal.Client.Repository
{
    public interface IJobRepo
    {
        //Task<PagedList<JobDTO>> SearchJob(JobSearchParameter dto);
        Task<PagingResponse<JobDTO>> SearchJob(JobSearchParameter dto);
        Task<PagingResponse<JobDTO>> GetDefaultJobList(JobSearchParameter parameters);
        Task<ResponseStatusData> ApplyJobApplication(ApplicationDTO dto);
        Task<ResponseStatusData> GetJobDetailsById(long JobERPRecId, string SCandidateEmail);
        Task<ResponseStatusData> GetJobDetailsByJobId(long JobERPRecId);

        Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string SCandidateEmail);
        Task<ResponseStatusData> GetAppliedJobByStatus(Int16 ApplicationStatus, long? Number);

    }
}

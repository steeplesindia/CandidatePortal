using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Helpers;

namespace CandidatePortal.Server.Repositories
{
    public interface IJobService
    {
        //Task<ResponseStatusData> AddJobAsync(JobDTO item);

        //Task<ResponseStatusData> EditJobAsync(JobDTO item);

        //Task<ResponseStatusData> DeleteJobAsync(long ERPRecId);

        //Task<ResponseStatusData> SearchJob(string SearchTerm, int PageNumber, int PageSize);
        Task<PagedList<JobDTO>> SearchJob(string SearchTerm, string SerachLocation, int PageNumber, int PageSize);

        Task<ResponseStatusData> GetAppliedJobByDateRange(DateTime fromDate, DateTime toDate, long nCandidateId);

        Task<ResponseStatusData> GetAppliedJobByStatus(Int16 ApplicationStatus, long nCandidateId);

        Task<ResponseStatusData> GetJobDetailsById(long JobERPRecId, string SCandidateEmail);
        Task<ResponseStatusData> GetJobDetailsByJobId(long JobERPRecId);

        Task<PagedList<JobDTO>> DefaultJobList(int PageNumber, int PageSize);

        Task<ResponseStatusData> GetApplicationJobDetails(long ApplicationNumber);

        Task<ResponseStatusData> UpdateJobUrl(long jobErpRecId);
    }
}

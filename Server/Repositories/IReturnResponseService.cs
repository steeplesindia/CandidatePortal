
using CandidatePortal.Shared.DTO;

namespace CandidatePortal.Server.Repositories
{
    public interface IReturnResponseService
    {
        ResponseStatusData ReturnResponse(object data, long nID, int sMessageIndex, string sErrorStatus, int StatusCode, bool IsSuccess);
    
    }
}

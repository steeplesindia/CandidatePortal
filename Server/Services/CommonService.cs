using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.PortalUtilities;

namespace CandidatePortal.Server.Services
{
    public class ReturnResponseService : IReturnResponseService
    {
        private readonly IPortalUtilities _portalUtilities;
        private readonly PortalDbContext _context;
        private readonly ILogger<ReturnResponseService> _logger;

        public ReturnResponseService(PortalDbContext context, IPortalUtilities portalUtilities, ILogger<ReturnResponseService> logger)
        {
            _context = context;
            _portalUtilities = portalUtilities;
            _logger = logger;
        }
        public ResponseStatusData ReturnResponse(object data, long nID, int sMessageIndex, string sErrorStatus, int StatusCode, bool IsSuccess)
        {
            ResponseStatusData responseStatus = new()
            {
                Data = data,
                Id = nID,
                Message = _portalUtilities.ErrorCodeByIndex(sMessageIndex),
                Status = sErrorStatus,
                StatusCode = StatusCode,
                IsSuccess = IsSuccess
            };

            _logger.LogInformation(responseStatus.Message);

            return responseStatus;
        }


        //public ResponseStatus GetResponseStatusWithStatusCode(int nID, int sMessageIndex, string sErrorStatus, int StatusCode, bool bRigths=false)
        //{
        //    ResponseStatus responseStatus = new ResponseStatus();

        //    responseStatus.nID = nID;
        //    responseStatus.sMessage = portalUtilities.ErrorCodeByIndex(sMessageIndex);
        //    responseStatus.sStatus = sErrorStatus;
        //    responseStatus.StatusCode = StatusCode;
        //    responseStatus.bRigths = bRigths;

        //    return responseStatus;
        //}

    }
}

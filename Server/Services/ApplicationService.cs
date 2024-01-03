using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public ApplicationService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> ApplyJobAsync(ApplicationDTO item)
        {
            item.AppliedDate = DateTime.Now;
            var entity = item.Adapt<Application>();
            entity.IsSynced = 0;

            await _context.Applications.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.Number, 16, ErrorStatus.Success, 200, true);
        }

        public bool CheckDuplicateApplicationAsync(long CandidateNumber, long JobERPRecId)
        { return _context.Applications.AsNoTracking().Where(x => x.CandidateNumber == CandidateNumber && x.JobERPRecId == JobERPRecId).Count() > 0 ? false : true; }
        
    }
}

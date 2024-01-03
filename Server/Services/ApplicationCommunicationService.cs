using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class ApplicationCommunicationService : IApplicationCommunicationService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public ApplicationCommunicationService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> AddAppsCommunicationAsync(ApplicationCommunicationAddDTO item)
        {
            //try
            //{
            var entity = item.Adapt<ApplicationCommunication>();

            entity.SubmittedDateTime = DateTime.Now;
            entity.Submitted = 1;
            entity.IsSynced = 0;

            entity.ApplicationCommunicationDocuments = item.ApplicationCommunicationDocumentAddDTO.Adapt<ICollection<ApplicationCommunicationDocument>>();
            
            if(entity.ApplicationCommunicationDocuments.Count() > 0)
                entity.ApplicationCommunicationDocuments.ToList().ForEach(item => item.IsSynced = 0);

            await _context.ApplicationCommunications.AddAsync(entity);
            var data = await _context.SaveChangesAsync();

            if (data != 0)
            {
                return _returnResponse.ReturnResponse(item, entity.Number, 20, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(item, entity.Number, 21, ErrorStatus.Warning, 200, true);
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public async Task<ResponseStatusData> EditAppsCommunicationAsync(ApplicationCommunicationAddDTO item)
        {
            var Data = await _context.ApplicationCommunications.FindAsync(item.Number);
            if (Data is not null)
            {
                //Data.ApplicationNumber = item.ApplicationNumber;
                //Data.CommunicationDirection = item.CommunicationDirection;
                //Data.Submitted = item.Submitted;

                Data.Subject = item.Subject;
                Data.Message = item.Message;
                Data.SubmittedDateTime = item.SubmittedDateTime;
                Data.IsSynced = 0;
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, Data.Number, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 400, false);
            }
        }

        public async Task<ResponseStatusData> DeleteAppCommunicaionAsync(long Number)
        {
            var Data = await _context.ApplicationCommunications.FirstOrDefaultAsync(b => b.Number == Number);
            if (Data is not null)
            {
                _context.ApplicationCommunications.Remove(Data);
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 400, false);
            }
        }

        public async Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(int ApplicationNumber)
        {
            //try
            //{
            IEnumerable<ApplicationCommunicationAddDTO> ApplicationCommunictionAddDTO =
                await (from C in _context.ApplicationCommunications.AsNoTracking()
                       join A in _context.Applications.AsNoTracking() on C.ApplicationNumber equals A.Number
                       join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId
                       where C.ApplicationNumber == ApplicationNumber
                       select new ApplicationCommunicationAddDTO
                       {
                           Number = C.Number,
                           ApplicationNumber = C.ApplicationNumber,
                           CommunicationDirection = C.CommunicationDirection,
                           Subject = C.Subject,
                           Message = C.Message,
                           Submitted = C.Submitted,
                           SubmittedDateTime = C.SubmittedDateTime,
                           JobTitle = J.Code,
                           ApplicationCommunicationDocumentAddDTO =
                                   (from D in _context.ApplicationCommunicationDocuments
                                    where C.Number == D.ApplicationCommunicationNumber
                                    select new ApplicationCommunicationDocumentAddDTO
                                    {
                                        DocumentName = D.DocumentName,
                                        DocumentPath = D.DocumentPath,
                                    }).ToList()
                       }).OrderByDescending(x => x.Number).ToListAsync();

            return ApplicationCommunictionAddDTO == null
                ? _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false)
                : _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public async Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(int Number)
        {
            //try
            //{
            ApplicationCommunicationAddDTO ApplicationCommunictionAddDTO =
                await (from C in _context.ApplicationCommunications.AsNoTracking()
                       where C.Number == Number
                       select new ApplicationCommunicationAddDTO
                       {
                           Number = C.Number,
                           ApplicationNumber = C.ApplicationNumber,
                           CommunicationDirection = C.CommunicationDirection,
                           Subject = C.Subject,
                           Message = C.Message,
                           Submitted = C.Submitted,
                           SubmittedDateTime = C.SubmittedDateTime,

                       }).FirstOrDefaultAsync();

            return ApplicationCommunictionAddDTO == null
                 ? _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false)
                 : _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}

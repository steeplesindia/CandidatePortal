using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class ApplicationCheckListService : IApplicationCheckListService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public ApplicationCheckListService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> AddAppsCommunicationAsync(ApplicationChecklistAddDTO item)
        {
            try
            {
                var entity = item.Adapt<ApplicationChecklist>();
                //entity.SubmittedDateTime = DateTime.Now;
                //entity.Submitted = true;
                entity.IsSynced = 0;
                entity.ApplicationChecklistDocuments = item.ApplicationChecklistDocumentDTOs.Adapt<ICollection<ApplicationChecklistDocument>>();

                if (entity.ApplicationChecklistDocuments.Count() > 0)
                    entity.ApplicationChecklistDocuments.ToList().ForEach(item => item.IsSynced = 0);

                await _context.ApplicationChecklists.AddAsync(entity);
                var data = await _context.SaveChangesAsync();

                if (data != null)
                {
                    return _returnResponse.ReturnResponse(item, entity.Number, 8, ErrorStatus.Success, 200, true);
                }
                else
                {
                    return _returnResponse.ReturnResponse(item, entity.Number, 9, ErrorStatus.Warning, 200, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void AddAppsCommunicationDocAsync(List<ApplicationChecklistDocumentAddDTO> items, long Number, long ApplicationNumber)
        {
            try
            {
                foreach (var item in items)
                {
                    var entity = item.Adapt<ApplicationChecklistDocument>();
                    entity.Number = Number;
                    entity.ApplicationChecklistNumber = ApplicationNumber;
                    entity.DocumentPath = item.DocumentPath;
                    entity.DocumentName = item.DocumentName;
                    entity.IsSynced = 0;

                    await _context.ApplicationChecklistDocuments.AddAsync(entity);
                }
                //if (items != null)
                //{
                //    return _returnResponse.ReturnResponse(items, Number, 8, ErrorStatus.Success, 200, true);
                //}
                //else
                //{
                //    return _returnResponse.ReturnResponse(items, Number, 9, ErrorStatus.Warning, 200, true);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseStatusData> EditAppsCommunicationAsync(ApplicationChecklistAddDTO item)
        {
            var Data = await _context.ApplicationChecklists.FindAsync(item.Number);
            if (Data is not null)
            {
                Data.Completed = item.Completed;
                if (item.Completed == 1)
                {
                    Data.CompletedDateTime = DateTime.Now;
                }
                else
                {
                    Data.Remarks = item.Remarks;
                }

                Data.Status = Convert.ToInt16(item.Status);
                Data.IsSynced = 0;
                Data.ApplicationChecklistDocuments = item.ApplicationChecklistDocumentDTOs.Adapt<ICollection<ApplicationChecklistDocument>>();
                Data.ApplicationChecklistDocuments.ToList().ForEach(item => item.IsSynced = 0);

                await _context.SaveChangesAsync();

                //await AddAppsCommunicationDocAsync(item.ApplicationChecklistDocumentDTOs.Adapt<ICollection<ApplicationChecklistDocument>>(), item.Number, item.ApplicationNumber);

                return _returnResponse.ReturnResponse(Data, Data.Number, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> DeleteAppCommunicaionAsync(long Number)
        {
            var Data = await _context.ApplicationChecklists.FirstOrDefaultAsync(b => b.Number == Number);
            if (Data is not null)
            {
                _context.ApplicationChecklists.Remove(Data);
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Success, 400, false);
            }
        }

        public async Task<ResponseStatusData> GetAppCommunicaionByAppsNumberAsync(long NApplicationNumber)
        {
            List<ApplicationChecklistAddDTO> ApplicationCommunictionAddDTO = new List<ApplicationChecklistAddDTO>();
            try
            {
                ApplicationCommunictionAddDTO = (from C in _context.ApplicationChecklists.AsNoTracking()
                                                 join A in _context.Applications.AsNoTracking() on C.ApplicationNumber equals A.Number
                                                 join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId
                                                 where C.ApplicationNumber == NApplicationNumber
                                                 select new ApplicationChecklistAddDTO
                                                 {
                                                     Number = C.Number,
                                                     ApplicationNumber = C.ApplicationNumber,
                                                     Subject = C.Subject,
                                                     Description = C.Description,
                                                     Remarks = C.Remarks,
                                                     Status = C.Status,
                                                     JobTitle = J.Code,
                                                     Completed = C.Completed,
                                                     ApplicationChecklistDocumentDTOs = (from D in _context.ApplicationChecklistDocuments
                                                                                         where C.Number == D.ApplicationChecklistNumber
                                                                                         select new ApplicationChecklistDocumentAddDTO
                                                                                         {
                                                                                             DocumentName = D.DocumentName,
                                                                                             DocumentPath = D.DocumentPath
                                                                                         }).ToList()

                                                 }).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ApplicationCommunictionAddDTO == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetAppCommunicaionByNumberAsync(int Number)
        {
            ApplicationChecklistAddDTO ApplicationCommunictionAddDTO = new ApplicationChecklistAddDTO();
            try
            {
                ApplicationCommunictionAddDTO = (from C in _context.ApplicationChecklists.AsNoTracking()
                                                 where C.Number == Number
                                                 select new ApplicationChecklistAddDTO
                                                 {
                                                     Number = C.Number,
                                                     ApplicationNumber = C.ApplicationNumber,
                                                     Subject = C.Subject,
                                                     Description = C.Description,
                                                     Remarks = C.Remarks,
                                                     Status = C.Status,
                                                     Completed = C.Completed,
                                                     ApplicationChecklistDocumentDTOs =
                                                           (from D in _context.ApplicationChecklistDocuments
                                                            where C.Number == D.ApplicationChecklistNumber
                                                            select new ApplicationChecklistDocumentAddDTO
                                                            {
                                                                DocumentName = D.DocumentName,
                                                                DocumentPath = D.DocumentPath,
                                                            }).ToList()
                                                 }).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ApplicationCommunictionAddDTO == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

    }
}

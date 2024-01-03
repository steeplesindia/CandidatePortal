using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class JobService : IJobService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobService(PortalDbContext context, IReturnResponseService returnResponse, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _returnResponse = returnResponse;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedList<JobDTO>> SearchJob(string SearchTerm, string SearchLocation, int PageNumber, int PageSize)
        {
            var todayDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

            IQueryable<JobDTO> Data = from J in _context.Jobs.AsNoTracking()
                                      where J.Status == 1 && J.PublishedToPortal == 1
                                      select new JobDTO
                                      {
                                          JobERPRecId = J.ERPRecId,
                                          Code = J.Code,
                                          Description = J.Description,
                                          LongDescription = J.LongDescription,
                                          OpeningDate = J.OpeningDate,
                                          ClosingDate = J.ClosingDate,
                                          DataArea = J.DataArea,
                                          Locations = J.Locations,
                                          ApplicationDeadline = J.ApplicationDeadline,
                                          //IsSynced = 0,
                                      };
            if (Data != null && Data.Count() > 0)
            {
                Data = Data.ToList().Where(x => (DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}", x.OpeningDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                <= DateTime.ParseExact(todayDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                &&
                                DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}", x.ClosingDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                >= DateTime.ParseExact(todayDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))).AsQueryable();
            }

            if (Data != null && Data.Count() > 0)
            {
                if (SearchTerm != "null" && SearchLocation != "null")
                {
                    Data = Data.ToList().Where(x => x.Description.ToLower().Contains(SearchTerm.ToLower()) && x.Locations.ToLower().Contains(SearchLocation.ToLower())).AsQueryable();
                }
                else if (SearchLocation != "null")
                {
                    Data = Data.ToList().Where(x => x.Locations.ToLower().Contains(SearchLocation.ToLower())).AsQueryable();
                }
                else if (SearchTerm != "null")
                {
                    Data = Data.ToList().Where(x => x.Description.ToLower().Contains(SearchTerm.ToLower())).AsQueryable();
                }
            }


            //var outdata = PagedList<JobDTO>.ToPagedList(Data, PageNumber, PageSize);

            //if (outdata is null)
            //{
            //    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            //}
            //else
            //{
            //    return _returnResponse.ReturnResponse(outdata, 0, 13, ErrorStatus.Success, 200, true);
            //}
            return PagedList<JobDTO>.ToPagedList(Data, PageNumber, PageSize);
        }

        public async Task<PagedList<JobDTO>> DefaultJobList(int PageNumber, int PageSize)
        {
            var todayDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

            IQueryable<JobDTO> Data = from J in _context.Jobs.AsNoTracking()
                                      select new JobDTO
                                      {
                                          JobERPRecId = J.ERPRecId,
                                          Code = J.Code,
                                          Description = J.Description,
                                          LongDescription = J.LongDescription,
                                          OpeningDate = J.OpeningDate,
                                          ClosingDate = J.ClosingDate,
                                          DataArea = J.DataArea,
                                          ApplicationDeadline = J.ApplicationDeadline,
                                          Locations = J.Locations,
                                          //IsSynced = 0,
                                          //PublishedToPortal = J.PublishedToPortal,
                                      };

            if (Data != null && Data.Count() > 0)
            {
                Data = Data.ToList().Where(x => (DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}", x.OpeningDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                 <= DateTime.ParseExact(todayDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                 &&
                                 DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}", x.ClosingDate), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                 >= DateTime.ParseExact(todayDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))).AsQueryable();
            }

            //var outdata = PagedList<JobDTO>.ToPagedList(Data, PageNumber, PageSize);
            return PagedList<JobDTO>.ToPagedList(Data, PageNumber, PageSize);
        }

        public async Task<ResponseStatusData> GetAppliedJobByDateRange(DateTime fromDate, DateTime toDate, long nCandidateId)
        {
            IQueryable<JobDTO> data = Enumerable.Empty<JobDTO>().AsQueryable();

            data = (from A in _context.Applications.AsNoTracking()
                    join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId
                    where A.CandidateNumber == nCandidateId
                    select new JobDTO
                    {
                        Applications = J.Applications.Adapt<List<ApplicationDTO>>(),//(ICollection<ApplicationDTO>)J.Applications,
                        LongDescription = J.LongDescription,
                        Code = J.Code,
                        DataArea = J.DataArea,
                        Description = J.Description,
                        ERPRecId = J.ERPRecId,
                        //OpeningDate = J.OpeningDate,
                        //ClosingDate = J.ClosingDate,
                    }).AsQueryable();

            data = data.AsEnumerable().Where(x =>
            (DateTime.ParseExact(x.Applications.FirstOrDefault().AppliedDate.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            >= DateTime.ParseExact(fromDate.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            && (DateTime.ParseExact(x.Applications.FirstOrDefault().AppliedDate.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            <= DateTime.ParseExact(toDate.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
            ).AsQueryable();

            if (data is null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetAppliedJobByStatus(Int16 ApplicationStatus, long nCandidateId)
        {
            IQueryable<JobAppliedDTO> data = Enumerable.Empty<JobAppliedDTO>().AsQueryable();

            if (ApplicationStatus != 8) //Not All
            {
                data = (from A in _context.Applications.AsNoTracking()
                        join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId

                        //join AppQ in _context.ApplicationQuestionnaire on A.Number equals AppQ.ApplicationNumber into AQ1
                        //from AQMain in AQ1.DefaultIfEmpty()

                        where A.CandidateNumber == nCandidateId && A.ApplicationStatus == ApplicationStatus
                        select new JobAppliedDTO
                        {
                            LongDescription = J.LongDescription,
                            Code = J.Code,
                            DataArea = J.DataArea,
                            Description = J.Description,
                            ERPRecId = J.ERPRecId,
                            ApplicationStatus = A.ApplicationStatus,
                            AppliedDate = A.AppliedDate,
                            JobERPRecId = A.JobERPRecId,
                            ApplicationNumber = A.Number,
                            obj_applicationQuestionnaire =
                            
                            (from AQChild in _context.ApplicationQuestionnaire.AsNoTracking()
                             join QB in _context.ApplicationQuestionnaireLine.AsNoTracking() on AQChild.ERPRecId equals QB.ApplicationQuestionnaireErpRecId
                             where AQChild.ApplicationNumber == A.Number
                             select new ApplicationQuestionnaireDTO
                             {
                                 Completed = AQChild.Completed,
                                 Status = AQChild.Status,
                                 ApplicationNumber = AQChild.ApplicationNumber,
                                 ApplicationQuestionnaireLine = AQChild.ApplicationQuestionnaireLine.Select(x=> new ApplicationQuestionnaireLineDTO { ERPRecId = x.ERPRecId}).ToList()
                             }).FirstOrDefault(),

                            CheckListPendingCount = (from ACChild in _context.ApplicationChecklists.AsNoTracking()
                             where ACChild.ApplicationNumber == A.Number && ACChild.Completed == 0
                             select new ApplicationChecklistAddDTO
                             {
                                 Completed = ACChild.Completed,
                             }).Count(),
                            
                        }).AsQueryable();
            }
            else
            {
                data = (from A in _context.Applications.AsNoTracking()
                        join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId

                        //join AppQ in _context.ApplicationQuestionnaire on A.Number equals AppQ.ApplicationNumber into AQ1
                        //from AQMain in AQ1.DefaultIfEmpty()

                        where A.CandidateNumber == nCandidateId
                        select new JobAppliedDTO
                        {
                            LongDescription = J.LongDescription,
                            Code = J.Code,
                            DataArea = J.DataArea,
                            Description = J.Description,
                            ERPRecId = J.ERPRecId,
                            ApplicationStatus = A.ApplicationStatus,
                            AppliedDate = A.AppliedDate,
                            JobERPRecId = A.JobERPRecId,
                            ApplicationNumber = A.Number,
                            obj_applicationQuestionnaire =
                            (from AQChild in _context.ApplicationQuestionnaire.AsNoTracking()
                             join QB in _context.ApplicationQuestionnaireLine.AsNoTracking() on AQChild.ERPRecId equals QB.ApplicationQuestionnaireErpRecId
                             where AQChild.ApplicationNumber == A.Number
                             select new ApplicationQuestionnaireDTO
                             {
                                 Completed = AQChild.Completed,
                                 Status = AQChild.Status,
                                 ApplicationNumber = AQChild.ApplicationNumber,
                                 ApplicationQuestionnaireLine = AQChild.ApplicationQuestionnaireLine.Select(x => new ApplicationQuestionnaireLineDTO { ERPRecId = x.ERPRecId }).ToList()
                             }).FirstOrDefault(),

                            CheckListPendingCount = (from ACChild in _context.ApplicationChecklists.AsNoTracking()
                                                     where ACChild.ApplicationNumber == A.Number && ACChild.Completed == 0
                                                     select new ApplicationChecklistAddDTO
                                                     {
                                                         Completed = ACChild.Completed,
                                                     }).Count(),

                        }).AsQueryable();
            }

            if (data is null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetJobDetailsById(long JobERPRecId, string SCandidateEmail)
        {
            long CandidateNumber = 0;
            CandidateNumber = await (from J in _context.Candidates.AsNoTracking()
                                     where J.Email == SCandidateEmail
                                     select J.Number).FirstOrDefaultAsync();

            JobDTO data = await (from J in _context.Jobs.AsNoTracking()
                                 where J.ERPRecId == JobERPRecId
                                 select new JobDTO
                                 {
                                     JobERPRecId = J.ERPRecId,
                                     Code = J.Code,
                                     Description = J.Description,
                                     LongDescription = J.LongDescription,
                                     OpeningDate = J.OpeningDate,
                                     ClosingDate = J.ClosingDate,
                                     DataArea = J.DataArea,
                                     CandidateNumber = CandidateNumber,
                                     Locations = J.Locations,
                                     Status = J.Status,
                                     ApplicationDeadline = J.ApplicationDeadline,
                                     Applications = (from Ap in _context.Applications.AsNoTracking()
                                                     join Js in _context.Jobs.AsNoTracking() on Ap.JobERPRecId equals Js.ERPRecId
                                                     where Ap.CandidateNumber == CandidateNumber
                                                     && Ap.JobERPRecId == JobERPRecId
                                                     select new ApplicationDTO
                                                     {
                                                         CandidateNumber = Ap.CandidateNumber,
                                                         JobERPRecId = Ap.JobERPRecId,
                                                         ApplicationStatus = Ap.ApplicationStatus
                                                     }).ToList(),
                                 }).FirstOrDefaultAsync();

            if (data is null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetJobDetailsByJobId(long JobERPRecId) {
            JobDTO data = await (from J in _context.Jobs.AsNoTracking()
                                 where J.ERPRecId == JobERPRecId
                                 select new JobDTO
                                 {
                                     JobERPRecId = J.ERPRecId,
                                     Code = J.Code,
                                     Description = J.Description,
                                     LongDescription = J.LongDescription,
                                     OpeningDate = J.OpeningDate,
                                     ClosingDate = J.ClosingDate,
                                     DataArea = J.DataArea,
                                     Locations = J.Locations,
                                     Status = J.Status,
                                     ApplicationDeadline = J.ApplicationDeadline,
                                 }).FirstOrDefaultAsync();

            if (data is null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }


        public async Task<ResponseStatusData> GetApplicationJobDetails(long ApplicationNumber)
        {
            JobAppliedDTO data = new JobAppliedDTO();

            data = (from A in _context.Applications.AsNoTracking()
                    join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId
                    where A.Number == ApplicationNumber
                    select new JobAppliedDTO
                    {
                        LongDescription = J.LongDescription,
                        Code = J.Code,
                        Description = J.Description,
                        ApplicationStatus = A.ApplicationStatus,
                        AppliedDate = A.AppliedDate,
                        JobERPRecId = A.JobERPRecId,
                        ApplicationNumber = A.Number
                    }).FirstOrDefault();

            if (data is null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        //public async Task<ResponseStatusData> UpdateJobUrl(string sUrl)
        //{
        //    if (sUrl != "")
        //    {
        //        string[] splt = sUrl.Split('/');
        //        if (splt.Length < 3)
        //        {
        //            //Invalid url
        //            return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
        //        }

        //        int nId = 0;
        //        bool bFlag = int.TryParse(splt.Last(), out nId);
        //        if (bFlag)
        //        {
        //            var Data = await _context.Jobs.Where(x => x.ERPRecId == nId).FirstOrDefaultAsync();
        //            if (Data is null)
        //            {
        //                //Record not found for the id
        //                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
        //            }
        //            else
        //            {
        //                Data.PortalLink = sUrl;
        //                await _context.SaveChangesAsync();
        //                return _returnResponse.ReturnResponse(Data, Data.ERPRecId, 1, ErrorStatus.Success, 200, true);

        //                // do update job url and issync flag
        //                // return message
        //            }
        //        }
        //        else
        //        {
        //            return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
        //        }
        //    }
        //    else
        //    {
        //        //Invalid url
        //        return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
        //    }
        //}

        public async Task<ResponseStatusData> UpdateJobUrl(long jobErpRecId)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            string sUrl = new Uri(new Uri($"{request.Scheme}://{request.Host.Value}").ToString()).ToString();



            //string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            //string sUrl = _httpContextAccessor.HttpContext.Request.Path;// HttpContext.Current.Request.Url.AbsoluteUri;
            //https://localhost:7118/JobDetail/1
            if (sUrl != "")
            {
                //string[] splt = sUrl.Split('/');
                //if (splt.Length < 3)
                //{
                //    //Invalid url
                //    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                //}

                //int nId = 0;
                //bool bFlag = int.TryParse(splt.Last(), out nId);
                //if (bFlag)
                //{
                    var Data = await _context.Jobs.Where(x => x.ERPRecId == jobErpRecId).FirstOrDefaultAsync();
                    if (Data is null)
                    {
                        //Record not found for the id
                        return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                    }
                    else
                    {
                        Data.PortalLink = sUrl + "JobDetail/" + jobErpRecId.ToString();
                        await _context.SaveChangesAsync();
                        return _returnResponse.ReturnResponse(Data, Data.ERPRecId, 1, ErrorStatus.Success, 200, true);

                        // do update job url and issync flag
                        // return message
                    }
                //}
                //else
                //{
                //    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                //}
            }
            else
            {
                //Invalid url
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
        }

    }
}

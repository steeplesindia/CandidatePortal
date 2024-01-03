using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class ApplicationQuestionnairesService : IApplicationQuestionnairesService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public ApplicationQuestionnairesService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> EditAppsQuestionnaireAsync(ApplicationQuestionnaireDTO QuestionList)
        {
            foreach (var item in QuestionList.ApplicationQuestionnaireLine)
            {
                var Data = await _context.ApplicationQuestionnaireLine.FindAsync(item.ERPRecId);
                if (Data is not null)
                {
                    Data.Answer = item.Answer;
                    Data.Comments = item.Comments;
                    Data.AnswerErpRecId = item.AnswerErpRecId;
                    Data.IsSynced = 0;
                    await _context.SaveChangesAsync();
                }
            }

            var DataMain = await _context.ApplicationQuestionnaire.FindAsync(QuestionList.ERPRecId);
            if (DataMain is not null)
            {
                DataMain.StartDate = QuestionList.StartDate;
                DataMain.EndDate = QuestionList.EndDate;
                DataMain.Completed = 1;
                DataMain.Status = 2; // Completed
                DataMain.IsSynced = 0;
                await _context.SaveChangesAsync();
            }

            //Update Status
            //var DataMain = await _context.ApplicationQuestionnaire.FindAsync(QuestionList[0].ApplicationQuestionnaireErpRecId);
            //if (DataMain is not null)
            //{
            //    DataMain.Completed = 1;
            //    DataMain.Status = 2; // Completed
            //    DataMain.IsSynced = 0;
            //    await _context.SaveChangesAsync();
            //}

            //EditAppsQuestionnaireStatus(QuestionList[0].ApplicationQuestionnaireErpRecId);
            return _returnResponse.ReturnResponse(null, QuestionList.Number, 1, ErrorStatus.Success, 200, true);
        }

        public async void EditAppsQuestionnaireStatus(long ApplicationQuestionnaireErpRecId)
        {
            var Data = await _context.ApplicationQuestionnaire.FindAsync(ApplicationQuestionnaireErpRecId);
            if (Data is not null)
            {
                Data.Status = 2; // Completed
                Data.Completed = 1;
                Data.IsSynced = 0;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ResponseStatusData> DeleteAppQuestionnaireAsync(long Number)
        {
            var Data = await _context.ApplicationQuestionnaireLine.FirstOrDefaultAsync(b => b.Number == Number);
            if (Data is not null)
            {
                _context.ApplicationQuestionnaireLine.Remove(Data);
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 400, false);
            }
        }

        public async Task<ResponseStatusData> GetAppQuestionnaireByAppsNumberAsync(long ApplicationNumber)
        {
            IEnumerable<ApplicationQuestionnaireLineDTO> ApplicationCommunictionAddDTO =
                await (from AQ in _context.ApplicationQuestionnaire.AsNoTracking()
                       join AQL in _context.ApplicationQuestionnaireLine.AsNoTracking() on AQ.ERPRecId equals AQL.ApplicationQuestionnaireErpRecId
                       join A in _context.Applications.AsNoTracking() on AQ.ApplicationNumber equals A.Number
                       join J in _context.Jobs.AsNoTracking() on A.JobERPRecId equals J.ERPRecId
                       where AQ.ApplicationNumber == ApplicationNumber
                       select new ApplicationQuestionnaireLineDTO
                       {
                           Number = AQL.Number,
                           Text = AQL.Text,
                           Type = AQL.Type,
                           Answer = AQL.Answer,
                           Comments = AQL.Comments,
                           SequenceNumber = AQL.SequenceNumber,
                           ERPRecId = AQL.ERPRecId,
                          
                           ApplicationQuestionnaireErpRecId = AQ.ERPRecId,
                           ApplicationQuestionnaireLineAnswer = (from AQA in _context.ApplicationQuestionnaireLineAnswer.AsNoTracking()
                                                                 where AQA.ApplicationQuestionnaireLineErpRecId == AQL.ERPRecId
                                                                 select new ApplicationQuestionnaireLineAnswerDTO
                                                                 {
                                                                     ApplicationQuestionnaireLineErpRecId = AQA.ApplicationQuestionnaireLineErpRecId,
                                                                     Text = AQA.Text,
                                                                     SequenceNumber = AQA.SequenceNumber,
                                                                     ERPRecId = AQA.ERPRecId,
                                                                 }).ToList(),
                       }).OrderBy(x => x.SequenceNumber).ToListAsync();

            //AQL.ApplicationQuestionnaireLineAnswer.OrderBy(x=>x.SequenceNumber).ToList()
            return ApplicationCommunictionAddDTO == null
                ? _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false)
                : _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> GetAppQuestionnaireByNumberAsync(long Number)
        {
            //try
            //{
            ApplicationQuestionnaireLine ApplicationCommunictionAddDTO =
                await (from C in _context.ApplicationQuestionnaireLine.AsNoTracking()
                       where C.Number == Number
                       select new ApplicationQuestionnaireLine
                       {
                           Number = C.Number,
                           Text = C.Text,
                           Type = C.Type,
                           Answer = C.Answer,
                           Comments = C.Comments
                       }).FirstOrDefaultAsync();

            return ApplicationCommunictionAddDTO == null
                 ? _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false)
                 : _returnResponse.ReturnResponse(ApplicationCommunictionAddDTO, 0, 13, ErrorStatus.Success, 200, true);
        }
    }
}

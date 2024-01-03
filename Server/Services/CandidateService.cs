using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CandidatePortal.Server.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public CandidateService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> AddCandidateAsync(CandidateAddEditDTO item)
        {
            try
            {

                item.GeneratedDate = DateTime.Now;
                item.IsSynced = 0;
                var entity = item.Adapt<Candidate>();

                if (entity.CandidateExperiences.Count() > 0)
                    entity.CandidateExperiences.ToList().ForEach(item => item.IsSynced = 0);

                if (entity.CandidateEducations.Count() > 0)
                    entity.CandidateEducations.ToList().ForEach(item => item.IsSynced = 0);

                if (entity.CandidateSkills.Count() > 0)
                    entity.CandidateSkills.ToList().ForEach(item => item.IsSynced = 0);

                if (entity.CandidateCertificateTypes.Count() > 0)
                    entity.CandidateCertificateTypes.ToList().ForEach(item => item.IsSynced = 0);

                await _context.Candidates.AddAsync(entity);
                await _context.SaveChangesAsync();

                if (SendNewCandidateRegistrationMail())
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

        public async Task<ResponseStatusData> AddCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item)
        {
            try
            {
                item.IsSynced = 0;
                var entity = item.Adapt<CandidateCertificateType>();
                await _context.CandidateCertificateTypes.AddAsync(entity);
                await _context.SaveChangesAsync();

                var DATACerti = await GetCandidateCertificateByIdAsync(item.CandidateNumber, item.CertificateTypeERPRecId);
                return _returnResponse.ReturnResponse(DATACerti, 0, 0, ErrorStatus.Success, 200, true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseStatusData> AddCandidateEducationAsync(CandidateEducationAddEditDTO item)
        {

            try
            {
                item.IsSynced = 0;
                var entity = item.Adapt<CandidateEducation>();

                await _context.CandidateEducations.AddAsync(entity);
                await _context.SaveChangesAsync();

                var dataexp = await GetCandidateEducationByIdAsync(item.CandidateNumber, item.EducationERPRecId);
                return _returnResponse.ReturnResponse(dataexp, 0, 0, ErrorStatus.Success, 200, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseStatusData> AddCandidateExperienceAsync(CandidateExperienceDTO item)
        {
            item.IsSynced = 0;
            var entity = item.Adapt<CandidateExperience>();

            await _context.CandidateExperiences.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, 0, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddCandidateSkillAsync(CandidateSkillAddEditDTO item)
        {
            try
            {
                item.IsSynced = 0;
                var entity = item.Adapt<CandidateSkill>();

                await _context.CandidateSkills.AddAsync(entity);
                await _context.SaveChangesAsync();

                var data = await GetCandidateSkillByIdAsync(item.CandidateNumber, item.SkillERPRecId);
                return _returnResponse.ReturnResponse(data, 0, 0, ErrorStatus.Success, 200, true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseStatusData> DeleteCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId)
        {
            try
            {
                var Data = await _context.CandidateCertificateTypes.FirstOrDefaultAsync(b => b.CandidateNumber == CandidateNumber && b.CertificateTypeERPRecId == CertificateTypeERPRecId);
                if (Data is not null)
                {
                    Data.IsSynced = 0;
                    Data.IsDeleted = 1;
                    await _context.SaveChangesAsync();
                    
                    //_context.CandidateCertificateTypes.Remove(Data);
                    //await _context.SaveChangesAsync();

                    return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
                }
                else
                {
                    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Success, 400, false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseStatusData> DeleteCandidateEducationAsync(long CandidateNumber, long EducationERPRecId)
        {
            var Data = await _context.CandidateEducations.FirstOrDefaultAsync(b => b.CandidateNumber == CandidateNumber && b.EducationERPRecId == EducationERPRecId);
            if (Data is not null)
            {
                Data.IsSynced = 0;
                Data.IsDeleted = 1;
                await _context.SaveChangesAsync();
                //_context.CandidateEducations.Remove(Data);
                //await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Success, 400, false);
            }
        }

        public async Task<ResponseStatusData> DeleteCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            var Data = await _context.CandidateExperiences.FirstOrDefaultAsync(b => b.CandidateNumber == CandidateNumber && b.Employer.ToLower().Equals(Employer.ToLower()));
            if (Data is not null)
            {
                Data.IsSynced = 0;
                Data.IsDeleted = 1;
                await _context.SaveChangesAsync();

                //_context.CandidateExperiences.Remove(Data);
                //await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Success, 400, false);
            }
        }

        public async Task<ResponseStatusData> DeleteCandidateSkillAsync(long CandidateNumber, long SkillERPRecId)
        {
            try
            {
                var Data = await _context.CandidateSkills.FirstOrDefaultAsync(b => b.CandidateNumber == CandidateNumber && b.SkillERPRecId == SkillERPRecId);
                if (Data is not null)
                {
                    Data.IsSynced = 0;
                    Data.IsDeleted = 1;
                    await _context.SaveChangesAsync();

                    //_context.CandidateSkills.Remove(Data);
                    //await _context.SaveChangesAsync();

                    return _returnResponse.ReturnResponse(Data, 0, 2, ErrorStatus.Success, 200, true);
                }
                else
                {
                    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Success, 400, false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseStatusData> EditCandidateAsync(CandidateAddEditDTO item)
        {
            var Data = await _context.Candidates.FindAsync(item.Number);
            if (Data is not null)
            {
                //Data.CitizenShipCountry = item.CitizenShipCountry;
                Data.CityERPRecId = item.CityERPRecId;
                Data.ContactNo = item.ContactNo;
                Data.CountryERPRecId = item.CountryERPRecId;
                Data.CurrentJobTitle = item.CurrentJobTitle;
                Data.AlternateContactNo = item.AlternateContactNo;
                Data.Dob = item.Dob;
                Data.LinkedinLink = item.LinkedinLink;
                //Data.Purpose = item.Purpose;
                Data.FirstName = item.FirstName;
                Data.FaceBookLink = item.FaceBookLink;
                Data.MaritalStatus = item.MaritalStatus;
                Data.MiddleName = item.MiddleName;
                //Data.Activities = item.Activities;
                Data.Gender = item.Gender;
                Data.HighestDegreeERPRecId = item.HighestDegreeERPRecId;
                Data.Disabled = item.Disabled;// 04-06-2022
                Data.CanRelocate = item.CanRelocate;// 04-06-2022
                Data.CanTravel = item.CanTravel;
                Data.LastName = item.LastName;
                Data.PersonalTitleERPRecId = item.PersonalTitleERPRecId;
                Data.ImageURL = item.ImageURL;
                //Data.SearchEnginePolicy = item.SearchEnginePolicy;
                Data.StateERPRecId = item.StateERPRecId;
                Data.Street = item.Street;
                //Data.TellAboutUs = item.TellAboutUs;
                Data.TwitterLink = item.TwitterLink;
                Data.ResumeURL = item.ResumeURL;
                Data.NationalityId = item.NationalityId;
                Data.ZipCode = item.ZipCode;

                //Data.ERPRecId = item.ERPRecId;
                //Data.DataArea = item.DataArea;
                Data.IsSynced = 0;
                Data.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, Data.Number, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> EditCandidateCertificateAsync(CandidateCertificateTypeAddEditDTO item)
        {
            try
            {
                var Data = await _context.CandidateCertificateTypes.Where(x => x.CandidateNumber == item.CandidateNumber && x.CertificateTypeERPRecId == item.CertificateTypeERPRecId).FirstOrDefaultAsync();
                if (Data is not null)
                {
                    Data.StartDate = item.StartDate;
                    Data.EndDate = item.EndDate;
                    Data.Notes = item.Notes;
                    Data.RenewalRequired = item.RenewalRequired;
                    Data.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    var DataCerti = await GetCandidateCertificateByIdAsync(item.CandidateNumber, item.CertificateTypeERPRecId);

                    return _returnResponse.ReturnResponse(DataCerti, 0, 1, ErrorStatus.Success, 200, true);
                }
                else
                {
                    return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseStatusData> EditCandidateEducationAsync(CandidateEducationAddEditDTO item)
        {
            var Data = await _context.CandidateEducations.Where(x => x.CandidateNumber == item.CandidateNumber && x.EducationERPRecId == item.EducationERPRecId).FirstOrDefaultAsync();
            if (Data is not null)
            {
                //Data.EducationERPRecId = item.EducationERPRecId ?? 0;
                Data.StartDate = item.StartDate;
                Data.EndDate = item.EndDate;
                Data.Scale = item.Scale;
                Data.AvgGrade = item.AvgGrade;
                Data.IsSynced = 0;
                await _context.SaveChangesAsync();
                var dataexp = await GetCandidateEducationByIdAsync(Data.CandidateNumber, Data.EducationERPRecId);

                return _returnResponse.ReturnResponse(dataexp, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> EditCandidateExperienceAsync(CandidateExperienceDTO item)
        {
            var Data = await _context.CandidateExperiences.Where(x => x.CandidateNumber == item.CandidateNumber && x.Employer.ToLower().Equals(item.Employer.ToLower())).FirstOrDefaultAsync();
            if (Data is not null)
            {
                //Data.Employer = item.Employer;
                Data.Position = item.Position;
                Data.StartDate = item.StartDate;
                Data.EndDate = item.EndDate;
                Data.EmployerURL = item.EmployerURL;
                Data.EmployerLocation = item.EmployerLocation;
                Data.EmployerContactNo = item.EmployerContactNo;
                Data.Notes = item.Notes;
                Data.IsSynced = 0;
                await _context.SaveChangesAsync();

                return _returnResponse.ReturnResponse(Data, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> EditCandidateSkillAsync(CandidateSkillAddEditDTO item)
        {
            var Data = await _context.CandidateSkills.Where(x => x.CandidateNumber == item.CandidateNumber && x.SkillERPRecId == item.SkillERPRecId).FirstOrDefaultAsync();
            if (Data is not null)
            {
                try
                {
                    //Data.SkillERPRecId = item.SkillERPRecId ?? 0;
                    Data.SkillLevelERPRecId = item.SkillLevelERPRecId ?? 0;
                    Data.LevelDate = item.LevelDate;
                    Data.YearOfExperience = item.YearOfExperience;
                    Data.IsSynced = 0;
                    await _context.SaveChangesAsync();

                    var dataSkill = await GetCandidateSkillByIdAsync(item.CandidateNumber, item.SkillERPRecId);
                    return _returnResponse.ReturnResponse(dataSkill, 0, 1, ErrorStatus.Success, 200, true);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> GetCandidateAsync(string EmailId)
        {
            CandidateAddEditDTO candidateAddEditDTO = new CandidateAddEditDTO();
            try
            {
                candidateAddEditDTO = (from C in _context.Candidates.AsNoTracking()
                                       join CTT in _context.Countries.AsNoTracking() on C.CountryERPRecId equals CTT.ERPRecId into Country
                                       from Conr1 in Country.DefaultIfEmpty()

                                       join ST in _context.States.AsNoTracking() on C.StateERPRecId equals ST.ERPRecId into State
                                       from ST1 in State.DefaultIfEmpty()

                                       join CT in _context.Cities.AsNoTracking() on C.CityERPRecId equals CT.ERPRecId into City
                                       from CT1 in City.DefaultIfEmpty()

                                       join NTT in _context.Countries.AsNoTracking() on C.NationalityId equals NTT.ERPRecId into Nationality
                                       from Nati1 in Nationality.DefaultIfEmpty()

                                       where C.Email == EmailId
                                       select new CandidateAddEditDTO
                                       {
                                           PersonalTitleERPRecId = C.PersonalTitleERPRecId,
                                           FirstName = C.FirstName,
                                           MiddleName = C.MiddleName,
                                           LastName = C.LastName,
                                           CurrentJobTitle = C.CurrentJobTitle,
                                           HighestDegreeERPRecId = C.HighestDegreeERPRecId,
                                           PreviousEmployee = C.PreviousEmployee,
                                           CountryERPRecId = C.CountryERPRecId,
                                           StateERPRecId = C.StateERPRecId,
                                           CityERPRecId = C.CityERPRecId,
                                           Street = C.Street,
                                           Email = C.Email,
                                           ContactNo = C.ContactNo,
                                           AlternateContactNo = C.AlternateContactNo,
                                           ImageURL = C.ImageURL,
                                           ResumeURL = C.ResumeURL,
                                           FaceBookLink = C.FaceBookLink,
                                           TwitterLink = C.TwitterLink,
                                           LinkedinLink = C.LinkedinLink,
                                           Dob = C.Dob,
                                           Gender = C.Gender,
                                           MaritalStatus = C.MaritalStatus,
                                           Number = C.Number,
                                           Country = Conr1.Code,
                                           State = ST1.Code,
                                           City = CT1.Code,
                                           CanRelocate = C.CanRelocate,
                                           CanTravel = C.CanTravel,
                                           ZipCode = C.ZipCode,
                                           NationalityId = C.NationalityId,
                                           Nationality = Nati1.Code,

                                           Disabled = C.Disabled,
                                           IsSynced = C.IsSynced,

                                           CandidateExperiences = (from CE in _context.CandidateExperiences.AsNoTracking()
                                                                   where CE.CandidateNumber == C.Number && CE.IsDeleted == 0
                                                                   select new CandidateExperienceDTO
                                                                   {
                                                                       CandidateNumber = CE.CandidateNumber,
                                                                       Employer = CE.Employer,
                                                                       Position = CE.Position,
                                                                       EmployerURL = CE.EmployerURL,
                                                                       EmployerContactNo = CE.EmployerContactNo,
                                                                       EmployerLocation = CE.EmployerLocation,
                                                                       StartDate = CE.StartDate,
                                                                       EndDate = CE.EndDate,
                                                                       Notes = CE.Notes,
                                                                       //IsSynced = CE.IsSynced,
                                                                       //RowVersion = CE.RowVersion,
                                                                   }).ToList(),

                                           CandidateEducations = (from CE in _context.CandidateEducations.AsNoTracking()
                                                                  join CMs in _context.Educations.AsNoTracking() on CE.EducationERPRecId equals CMs.ERPRecId
                                                                  where CE.CandidateNumber == C.Number && CE.IsDeleted == 0
                                                                  select new CandidateEducationAddEditDTO
                                                                  {
                                                                      CandidateNumber = CE.CandidateNumber,
                                                                      EducationERPRecId = CE.EducationERPRecId,
                                                                      StartDate = CE.StartDate,
                                                                      EndDate = CE.EndDate,
                                                                      AvgGrade = CE.AvgGrade,
                                                                      Scale = CE.Scale,
                                                                      //IsSynced = CE.IsSynced,
                                                                      //RowVersion = CE.RowVersion,
                                                                      Code = CMs.Code,
                                                                  }).ToList(),

                                           CandidateSkills = (from CS in _context.CandidateSkills.AsNoTracking()
                                                              join Sks in _context.Skills.AsNoTracking() on CS.SkillERPRecId equals Sks.ERPRecId

                                                              join Skl in _context.SkillLevels.AsNoTracking() on CS.SkillLevelERPRecId equals Skl.ERPRecId into Skill
                                                              from Sk1 in Skill.DefaultIfEmpty()

                                                              where CS.CandidateNumber == C.Number && CS.IsDeleted == 0
                                                              select new CandidateSkillAddEditDTO
                                                              {
                                                                  CandidateNumber = CS.CandidateNumber,
                                                                  SkillERPRecId = CS.SkillERPRecId,
                                                                  SkillLevelERPRecId = CS.SkillLevelERPRecId,
                                                                  LevelDate = CS.LevelDate,
                                                                  YearOfExperience = CS.YearOfExperience,
                                                                  SkillCode = Sks.Code,
                                                                  SkillLevelCode = Sk1.Code,
                                                                  //IsSynced = CS.IsSynced,
                                                                  //RowVersion = CS.RowVersion,
                                                              }).ToList(),

                                           CandidateCertificateTypes = (from CS in _context.CandidateCertificateTypes.AsNoTracking()
                                                                        join certy in _context.CertificateTypes.AsNoTracking() on CS.CertificateTypeERPRecId equals certy.ERPRecId

                                                                        where CS.CandidateNumber == C.Number && CS.IsDeleted == 0
                                                                        select new CandidateCertificateTypeAddEditDTO
                                                                        {
                                                                            CandidateNumber = CS.CandidateNumber,
                                                                            CertificateTypeERPRecId = CS.CertificateTypeERPRecId,
                                                                            Notes = CS.Notes,
                                                                            StartDate = CS.StartDate,
                                                                            EndDate = CS.EndDate,
                                                                            Code = certy.Code,
                                                                            RenewalRequired = CS.RenewalRequired,
                                                                            //IsSynced = CS.IsSynced,
                                                                            //RowVersion = CS.RowVersion,
                                                                        }).ToList()
                                       }).AsNoTracking().FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (candidateAddEditDTO == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(candidateAddEditDTO, candidateAddEditDTO.Number, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetCandidateAsync(long CandidateId)
        {
            var Data = await _context.Candidates
                 .Include(x => x.CandidateCertificateTypes).Where(x=> x.IsDeleted == 0)
                 .Include(x => x.CandidateEducations).Where(x => x.IsDeleted == 0)
                 .Include(x => x.CandidateExperiences).Where(x => x.IsDeleted == 0)
                 .Include(x => x.CandidateSkills).Where(x => x.IsDeleted == 0)
                 .FirstOrDefaultAsync(x => x.Number == CandidateId);

            if (Data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(Data, Data.Number, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetAllCandidatesAsync(CandidateParameters dto)
        {
            IQueryable<Candidate> Data = (IQueryable<Candidate>)_context.Candidates
                .Include(x => x.CandidateCertificateTypes).Where(x => x.IsDeleted == 0)
                .Include(x => x.CandidateEducations).Where(x => x.IsDeleted == 0)
                .Include(x => x.CandidateExperiences).Where(x => x.IsDeleted == 0)
                .Include(x => x.CandidateSkills).Where(x => x.IsDeleted == 0)
                .AsQueryable();

            var outdata = PagedList<Candidate>.ToPagedList(Data, dto.PageNumber, dto.PageSize);

            if (outdata == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                var outd = outdata.Adapt<List<CandidateDTO>>();
                return _returnResponse.ReturnResponse(outd, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<bool> CheckCandidateEmailAvailability(string EmailId)
        {
            //return _context.Candidates.AsNoTracking().ToList().Exists(p => p.Email.ToLower().Equals(EmailId.ToLower())) ? true : false;
            var data = await _context.Candidates.AsNoTracking().Where(x => x.Email.ToLower().Equals(EmailId.ToLower())).ToListAsync();
            return data.Count > 0;
        }

        public async Task<CandidateDetails> GetCandidateDetailsByEmailIdNew(string EmailId)
        {
            try
            {
                CandidateDetails obj_Candidate = (from C in _context.Candidates
                                                  where C.Email.ToLower() == EmailId.ToLower()
                                                  select new CandidateDetails()
                                                  {
                                                      FirstName = C.FirstName,
                                                      MiddleName = C.MiddleName,
                                                      Email = C.Email,
                                                      ImageURL = C.ImageURL,
                                                      ContactNo = C.ContactNo,
                                                      LastName = C.LastName,
                                                      Number = C.Number
                                                  }).FirstOrDefault();

                return obj_Candidate;
                //if (obj_Candidate == null)
                //{
                //    return _returnResponse.ReturnResponse(obj_Candidate, 0, 6, ErrorStatus.Warning, 204, false);
                //}
                //else
                //{
                //    return _returnResponse.ReturnResponse(obj_Candidate, 0, 13, ErrorStatus.Success, 200, true);
                //}
                //var data = await _context.Candidates.AsNoTracking().Where(x => x.Email.ToLower().Equals(EmailId.ToLower())).ToListAsync();
                //return data.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckCandidateEducationAvailability(long CandidateNumber, long? EducationERPRecId, long CandidateEduId = 0)
        {
            //if (CandidateEduId == 0) // 0 : Add
            //{ 
            return _context.CandidateEducations.AsNoTracking().Where(x => x.CandidateNumber == CandidateNumber && x.EducationERPRecId == EducationERPRecId && x.IsDeleted == 0).Count() > 0 ? false : true;
            //}
            //else { return _context.CandidateEducations.AsNoTracking().Where(x => x.CandidateNumber == CandidateNumber && x.EducationERPRecId == EducationERPRecId && x.CandidateEduId != CandidateEduId).Count() > 0 ? false : true; }
        }

        public bool CheckCandidateSkillAvailability(long CandidateNumber, long SkillERPRecId, long CandidateSkillId = 0)
        {
            //if (CandidateSkillId == 0)
            //{ 
            return _context.CandidateSkills.AsNoTracking().Where(x => x.CandidateNumber == CandidateNumber && x.SkillERPRecId == SkillERPRecId && x.IsDeleted == 0).Count() > 0 ? false : true;
            //}
            //else { return _context.CandidateSkills.AsNoTracking().Where(x => x.CandidateNumber == CandidateNumber && x.SkillERPRecId == SkillERPRecId && x.CandidateSkillId != CandidateSkillId).Count() > 0 ? false : true; }
        }

        private bool SendNewCandidateRegistrationMail()
        {
            return true;
        }

        public async Task<ResponseStatusData> GetCandidateEducationAsync(long CandidateNumber, long EducationERPRecId)
        {
            var Data = await _context.CandidateEducations.Where(x => x.CandidateNumber == CandidateNumber && x.EducationERPRecId == EducationERPRecId && x.IsDeleted == 0).FirstOrDefaultAsync();
            if (Data is not null)
            {
                return _returnResponse.ReturnResponse(Data, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<CandidateEducationAddEditDTO> GetCandidateEducationByIdAsync(long CandidateNumber, long? EducationERPRecId)
        {
            CandidateEducationAddEditDTO objDt =
               await (from CE in _context.CandidateEducations.AsNoTracking()
                      join CMs in _context.Educations.AsNoTracking() on CE.EducationERPRecId equals CMs.ERPRecId
                      where CE.CandidateNumber == CandidateNumber && CE.EducationERPRecId == EducationERPRecId && CE.IsDeleted == 0
                      select new CandidateEducationAddEditDTO
                      {
                          CandidateNumber = CE.CandidateNumber,
                          EducationERPRecId = CE.EducationERPRecId,
                          StartDate = CE.StartDate,
                          EndDate = CE.EndDate,
                          AvgGrade = CE.AvgGrade,
                          Scale = CE.Scale,
                          Code = CMs.Code,
                      }).FirstOrDefaultAsync();
            return objDt;
        }

        public async Task<ResponseStatusData> GetCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            var Data = await _context.CandidateExperiences.Where(x => x.CandidateNumber == CandidateNumber && x.Employer.ToLower().Equals(Employer.ToLower()) && x.IsDeleted == 0).FirstOrDefaultAsync();
            if (Data is not null)
            {
                return _returnResponse.ReturnResponse(Data, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<ResponseStatusData> GetCandidateSkillAsync(long CandidateNumber, long SkillERPRecId)
        {
            var Data = await _context.CandidateSkills.Where(x => x.CandidateNumber == CandidateNumber && x.SkillERPRecId == SkillERPRecId && x.IsDeleted == 0).FirstOrDefaultAsync();
            if (Data is not null)
            {
                return _returnResponse.ReturnResponse(Data, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<CandidateSkillAddEditDTO> GetCandidateSkillByIdAsync(long CandidateNumber, long? SkillERPRecId)
        {
            CandidateSkillAddEditDTO CandidateSkills =
                await (from CS in _context.CandidateSkills.AsNoTracking()
                       join Sks in _context.Skills.AsNoTracking() on CS.SkillERPRecId equals Sks.ERPRecId
                       join Skl in _context.SkillLevels.AsNoTracking() on CS.SkillLevelERPRecId equals Skl.ERPRecId into Skill
                       from Sk1 in Skill.DefaultIfEmpty()
                       where CS.CandidateNumber == CandidateNumber && CS.SkillERPRecId == SkillERPRecId && CS.IsDeleted == 0
                       select new CandidateSkillAddEditDTO
                       {
                           CandidateNumber = CS.CandidateNumber,
                           SkillERPRecId = CS.SkillERPRecId,
                           SkillLevelERPRecId = CS.SkillLevelERPRecId,
                           LevelDate = CS.LevelDate,
                           YearOfExperience = CS.YearOfExperience,
                           SkillCode = Sks.Code,
                           SkillLevelCode = Sk1.Code,
                       }).FirstOrDefaultAsync();
            return CandidateSkills;
        }

        public async Task<ResponseStatusData> GetCandidateCertificateAsync(long CandidateNumber, long CertificateTypeERPRecId)
        {
            var Data = await _context.CandidateCertificateTypes.Where(x => x.CandidateNumber == CandidateNumber && x.CertificateTypeERPRecId == CertificateTypeERPRecId && x.IsDeleted == 0).FirstOrDefaultAsync();
            if (Data is not null)
            {

                return _returnResponse.ReturnResponse(Data, 0, 1, ErrorStatus.Success, 200, true);
            }
            else
            {
                return _returnResponse.ReturnResponse(null, 0, 4, ErrorStatus.Error, 400, false);
            }
        }

        public async Task<CandidateCertificateTypeAddEditDTO> GetCandidateCertificateByIdAsync(long CandidateNumber, long? CertificateTypeERPRecId)
        {
            CandidateCertificateTypeAddEditDTO CandidateCertificateTypes =
                await (from CS in _context.CandidateCertificateTypes.AsNoTracking()
                       join certy in _context.CertificateTypes.AsNoTracking() on CS.CertificateTypeERPRecId equals certy.ERPRecId
                       where CS.CandidateNumber == CandidateNumber && CS.CertificateTypeERPRecId == CertificateTypeERPRecId && CS.IsDeleted == 0
                       select new CandidateCertificateTypeAddEditDTO
                       {
                           CandidateNumber = CS.CandidateNumber,
                           CertificateTypeERPRecId = CS.CertificateTypeERPRecId,
                           Notes = CS.Notes,
                           StartDate = CS.StartDate,
                           EndDate = CS.EndDate,
                           Code = certy.Code,
                           RenewalRequired = CS.RenewalRequired,
                       }).FirstOrDefaultAsync();

            return CandidateCertificateTypes;
        }
    }
}

using CandidatePortal.Server.DB;
using CandidatePortal.Server.Repositories;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CandidatePortal.Server.Services
{
    public class MasterService : IMasterService
    {
        public string DrpTitle = "-- Please Select --";

        private readonly PortalDbContext _context;
        private readonly IReturnResponseService _returnResponse;

        public MasterService(PortalDbContext context, IReturnResponseService returnResponse)
        {
            _context = context;
            _returnResponse = returnResponse;
        }

        public async Task<ResponseStatusData> GetCountriesAsync()
        {
            var entity = await _context.Countries.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<CountryDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new CountryDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetStatesAsync()
        {
            var entity = await _context.States.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<StateDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new StateDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetCitiesAsync()
        {
            var entity = await _context.Cities.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<CityDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new CityDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetMasterListAsync()
        {
            MasterListDTO obj_MasterListDTO = new MasterListDTO();

            var entityPerT = await _context.PersonalTitles.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            obj_MasterListDTO.obj_PersonalTitleDTO = entityPerT.Adapt<List<PersonalTitleDTO>>();

            var entityCoun = await _context.Countries.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            obj_MasterListDTO.obj_CountryDTO = entityCoun.Adapt<List<CountryDTO>>();

            var entityEdu = await _context.Educations.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            obj_MasterListDTO.obj_EducationDTO = entityEdu.Adapt<List<EducationDTO>>();

            var entitySkill = await _context.Skills.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            obj_MasterListDTO.obj_SkillDTO = entitySkill.Adapt<List<SkillDTO>>();

            var entityCerType = await _context.CertificateTypes.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            obj_MasterListDTO.obj_CertificateTypeDTO = entityCerType.Adapt<List<CertificateTypeDTO>>();

            object Data = obj_MasterListDTO;

            if (obj_MasterListDTO == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(Data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetStatesByCountryIdAsync(long CountryId)
        {
            var entity = await _context.States.AsNoTracking().Where(x => x.CountryERPRecId == CountryId).OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<StateDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new StateDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetCitiesByStateIdAsync(long StateId)
        {
            var entity = await _context.Cities.AsNoTracking().Where(x => x.StateERPRecId == StateId).OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<CityDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new CityDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetCitiesByCountryIdAsync(long CountryId)
        {
            var entity = await _context.Cities.AsNoTracking().Where(x => x.CountryERPRecId == CountryId).OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<CityDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new CityDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetSkillBySkillIdAsync(long skillId)
        {
            var entity = await _context.SkillLevels.AsNoTracking().Where(x => x.ERPRecId == skillId).OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<SkillLevel>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new SkillLevel
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetEducationLevelAsync()
        {
            var entity = await _context.EducationLevel.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<EducationLevel>>();

            try
            {
                data.Insert(0, new EducationLevel
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });

                if (data == null)
                {
                    return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
                }
                else
                {
                    return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task<ResponseStatusData> GetEducationsAsync()
        {
            var entity = await _context.Educations.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<EducationDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new EducationDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetPersonalTitlesAsync()
        {
            var entity = await _context.PersonalTitles.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<PersonalTitleDTO>>();

            data.Insert(0, new PersonalTitleDTO
            {
                ERPRecId = 0,
                Code = DrpTitle,
            });

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetSkillsAsync()
        {
            var entity = await _context.Skills.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<SkillDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new SkillDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetSkillLevelsAsync()
        {
            var entity = await _context.SkillLevels.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<SkillLevelDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new SkillLevelDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> GetCertificateTypesAsync()
        {
            var entity = await _context.CertificateTypes.AsNoTracking().OrderBy(x => x.Code).ToListAsync();
            var data = entity.Adapt<List<CertificateTypeDTO>>();

            if (data == null)
            {
                return _returnResponse.ReturnResponse(null, 0, 6, ErrorStatus.Warning, 204, false);
            }
            else
            {
                data.Insert(0, new CertificateTypeDTO
                {
                    ERPRecId = 0,
                    Code = DrpTitle,
                });
                return _returnResponse.ReturnResponse(data, 0, 13, ErrorStatus.Success, 200, true);
            }
        }

        public async Task<ResponseStatusData> AddCountryAsync(CountryDTO item)
        {
            var entity = item.Adapt<Country>();

            await _context.Countries.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddStateAsync(StateDTO item)
        {
            var entity = item.Adapt<State>();

            await _context.States.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddCityAsync(CityDTO item)
        {
            var entity = item.Adapt<City>();

            await _context.Cities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddEducationAsync(EducationDTO item)
        {
            var entity = item.Adapt<Education>();

            await _context.Educations.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddPersonalTitleAsync(PersonalTitleDTO item)
        {
            var entity = item.Adapt<PersonalTitle>();

            await _context.PersonalTitles.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddSkillAsync(SkillDTO item)
        {
            var entity = item.Adapt<Skill>();

            await _context.Skills.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddSkillLevelAsync(SkillLevelDTO item)
        {
            var entity = item.Adapt<SkillLevel>();

            await _context.SkillLevels.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<ResponseStatusData> AddCertificateTypeAsync(CertificateTypeDTO item)
        {
            var entity = item.Adapt<CertificateType>();

            await _context.CertificateTypes.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _returnResponse.ReturnResponse(item, entity.ERPRecId, 0, ErrorStatus.Success, 200, true);
        }

        public async Task<Country> GetCountryAsync(string sName)
        {
            return await _context.Countries.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<State> GetStatesAsync(string sName, long CountryID)
        {
            return await _context.States.AsNoTracking().Where(x => x.CountryERPRecId == CountryID && x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<City> GetCityAsync(string sName, long StateID)
        {
            return await _context.Cities.AsNoTracking().Where(x => x.StateERPRecId == StateID && x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<Education> GetEducationAsync(string sName)
        {
            return await _context.Educations.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<PersonalTitle> GetPersonalTitleAsync(string sName)
        {
            return await _context.PersonalTitles.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<Skill> GetSkillAsync(string sName)
        {
            return await _context.Skills.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<SkillLevel> GetSkillLevelAsync(string sName)
        {
            return await _context.SkillLevels.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }

        public async Task<CertificateType> GetCertificateTypeAsync(string sName)
        {
            return await _context.CertificateTypes.AsNoTracking().Where(x => x.Code.Equals(sName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }
    }
}

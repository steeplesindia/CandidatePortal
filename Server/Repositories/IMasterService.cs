using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Server.Repositories
{
    public interface IMasterService
    {
        Task<ResponseStatusData> GetCountriesAsync();

        Task<ResponseStatusData> GetStatesAsync();

        Task<ResponseStatusData> GetStatesByCountryIdAsync(long CountryId);

        Task<ResponseStatusData> GetSkillBySkillIdAsync(long skillId);

        Task<ResponseStatusData> GetCitiesAsync();

        Task<ResponseStatusData> GetCitiesByStateIdAsync(long StateId);
        Task<ResponseStatusData> GetCitiesByCountryIdAsync(long CountryId);

        Task<ResponseStatusData> GetEducationLevelAsync();

        Task<ResponseStatusData> GetEducationsAsync();
        
        Task<ResponseStatusData> GetPersonalTitlesAsync();

        Task<ResponseStatusData> GetSkillsAsync();

        Task<ResponseStatusData> GetSkillLevelsAsync();

        Task<ResponseStatusData> GetCertificateTypesAsync();


        Task<ResponseStatusData> AddCountryAsync(CountryDTO item);

        Task<ResponseStatusData> AddStateAsync(StateDTO item);
        
        Task<ResponseStatusData> AddCityAsync(CityDTO item);

        Task<ResponseStatusData> AddEducationAsync(EducationDTO item);

        Task<ResponseStatusData> AddPersonalTitleAsync(PersonalTitleDTO item);

        Task<ResponseStatusData> AddSkillAsync(SkillDTO item);

        Task<ResponseStatusData> AddSkillLevelAsync(SkillLevelDTO item);

        Task<ResponseStatusData> AddCertificateTypeAsync(CertificateTypeDTO item);

        Task<Country> GetCountryAsync(string sName);

        Task<State> GetStatesAsync(string sName, long CountryID);

        Task<City> GetCityAsync(string sName, long StateID);

        Task<Education> GetEducationAsync(string sName);

        Task<PersonalTitle> GetPersonalTitleAsync(string sName);

        Task<Skill> GetSkillAsync(string sName);

        Task<SkillLevel> GetSkillLevelAsync(string sName);

        Task<CertificateType> GetCertificateTypeAsync(string sName);

        Task<ResponseStatusData> GetMasterListAsync();

    }
}

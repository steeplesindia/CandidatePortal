using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class MasterListDTO
    {
        public MasterListDTO()
        {
            obj_PersonalTitleDTO = new List<PersonalTitleDTO>();
            obj_CountryDTO = new List<CountryDTO>();
            obj_EducationDTO = new List<EducationDTO>();
            obj_SkillDTO = new List<SkillDTO>();
            obj_CertificateTypeDTO = new List<CertificateTypeDTO>();
        }

        public List<PersonalTitleDTO> obj_PersonalTitleDTO = new List<PersonalTitleDTO>();
        public List<CountryDTO> obj_CountryDTO = new List<CountryDTO>();
        public List<EducationDTO> obj_EducationDTO = new List<EducationDTO>();
        public List<SkillDTO> obj_SkillDTO = new List<SkillDTO>();
        public List<CertificateTypeDTO> obj_CertificateTypeDTO = new List<CertificateTypeDTO>();
    }
}

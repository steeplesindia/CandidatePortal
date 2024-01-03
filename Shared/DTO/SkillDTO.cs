 using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class SkillDTO: AXModelDTO
    {
        public SkillDTO()
        {
            CandidateSkill = new HashSet<CandidateSkillDTO>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }

        public ICollection<CandidateSkillDTO> CandidateSkill { get; set; }
    }
}

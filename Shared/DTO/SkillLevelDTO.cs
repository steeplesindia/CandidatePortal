namespace CandidatePortal.Shared.DTO
{
    public class SkillLevelDTO: AXModelDTO
    {
        public SkillLevelDTO()
        {
            CandidateSkill = new HashSet<CandidateSkillDTO>();
        }
        public string Code { get; set; }
        public string? Description { get; set; }

        public ICollection<CandidateSkillDTO> CandidateSkill { get; set; }
    }
}

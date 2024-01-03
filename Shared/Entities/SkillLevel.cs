
namespace CandidatePortal.Shared.Entities
{
    public class SkillLevel: AXModel
    {
        public SkillLevel()
        {
            CandidateSkill = new HashSet<CandidateSkill>();
        }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? DataArea { get; set; }

        public ICollection<CandidateSkill> CandidateSkill { get; set; }
    }
}

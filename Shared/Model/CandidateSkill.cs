namespace CandidatePortal.Shared.Model
{
    public class CandidateSkill: AXModel
    {
        public long CandidateNumber { get; set; }
        public Candidate  Candidate { get; set; }
        public long SkillERPRecId { get; set; }
        public Skill Skill { get; set; }
    }
}

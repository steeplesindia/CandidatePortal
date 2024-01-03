
namespace CandidatePortal.Shared.Entities
{
    public class CandidateSkill: AXModel
    {
        public long Number { get; set; } // 28-05-22
        public long CandidateNumber { get; set; }
        public long? SkillERPRecId { get; set; }
        public long? SkillLevelERPRecId { get; set; }
        public DateTime? LevelDate { get; set; }
        public decimal? YearOfExperience { get; set; }
        public int? IsSynced { get; set; }
        public int IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }
        //public Skill Skill { get; set; }
        //public SkillLevel SkillLevel { get; set; }
    }
}

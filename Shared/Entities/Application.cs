
namespace CandidatePortal.Shared.Entities
{
    public class Application: AXModel
    {
        public long Number { get; set; }
        public long CandidateNumber { get; set; }
        public long JobERPRecId { get; set; }
        public Int16? ApplicationStatus { get; set; }

        public DateTime? AppliedDate { get; set; }

        public byte[]? RowVersion { get; set; }
        //public Job jobs { get; set; }

        public int? IsSynced { get; set; } // 14-06-2022

        public ICollection<ApplicationCommunication> ApplicationCommunications { get; set; }

        public ICollection<ApplicationChecklist> ApplicationChecklists { get; set; }

        public ICollection<ApplicationQuestionnaire> ApplicationQuestionnaire { get; set; }

        public Application()
        {
            ApplicationCommunications = new HashSet<ApplicationCommunication>();
            ApplicationChecklists = new HashSet<ApplicationChecklist>();
            ApplicationQuestionnaire = new HashSet<ApplicationQuestionnaire>();
        }
    }
}

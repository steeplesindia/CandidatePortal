
namespace CandidatePortal.Shared.Entities
{
    public class ApplicationChecklist : AXModel
    {
        public long Number { get; set; }

        public long ApplicationNumber { get; set; }

        public string? Subject { get; set; } = "";

        public string? Description { get; set; } = "";

        public string? Remarks { get; set; } = ""; //Akshay 14-06-2022

        //public DateTime RepliedDateTime { get; set; }

        public Int16? Status { get; set; }  //Akshay 14-06-2022

        public int? Completed { get; set; }

        public DateTime? CompletedDateTime { get; set; }

        public int? IsSynced { get; set; } //Akshay 14-06-2022

        public byte[]? RowVersion { get; set; }

        public ICollection<ApplicationChecklistDocument> ApplicationChecklistDocuments { get; set; }

        public ApplicationChecklist()
        {
            ApplicationChecklistDocuments = new HashSet<ApplicationChecklistDocument>();
        }

    }
}

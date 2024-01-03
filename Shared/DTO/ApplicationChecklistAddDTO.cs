
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationChecklistAddDTO : AXModelDTO
    {
        public long Number { get; set; }

        public long ApplicationNumber { get; set; }

        public string? Subject { get; set; } = "";

        public string? Description { get; set; } = "";

        public string? Remarks { get; set; } = ""; //Akshay 14-06-2022

        public int? Completed { get; set; }

        public DateTime? CompletedDateTime { get; set; }

        public DateTime? SubmittedDateTime { get; set; }

        public DateTime? RepliedDateTime { get; set; }

        //public Int16 Status { get; set; }
        public long? Status { get; set; } //Akshay 14-06-2022
        public string? JobTitle { get; set; } = "";


        //public int IsSynced { get; set; }

        //public byte[]? RowVersion { get; set; }

        public ICollection<ApplicationChecklistDocumentAddDTO> ApplicationChecklistDocumentDTOs { get; set; }

        public ApplicationChecklistAddDTO()
        {
            ApplicationChecklistDocumentDTOs = new HashSet<ApplicationChecklistDocumentAddDTO>();
        }
    }
}

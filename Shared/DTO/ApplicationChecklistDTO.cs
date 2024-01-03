
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationChecklistDTO : AXModelDTO
    {
        public long Number { get; set; }

        public long ApplicationNumber { get; set; }

        public string? Subject { get; set; } = "";

        public string? Description { get; set; } = "";

        public string? Remarks { get; set; } = "";

        public DateTime? SubmittedDateTime { get; set; }

        public DateTime? RepliedDateTime { get; set; }

        public Int16? Status { get; set; }

        //public int IsSynced { get; set; }

        //public byte[]? RowVersion { get; set; }

        public ICollection<ApplicationChecklistDocumentDTO> ApplicationChecklistDocumentDTOs { get; set; }

        public ApplicationChecklistDTO()
        {
            ApplicationChecklistDocumentDTOs = new HashSet<ApplicationChecklistDocumentDTO>();
        }
    }
}

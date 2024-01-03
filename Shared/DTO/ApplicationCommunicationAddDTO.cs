
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationCommunicationAddDTO : AXModelDTO
    {
        public long Number { get; set; }

        public long ApplicationNumber { get; set; }

        public Int16? CommunicationDirection { get; set; }

        public string? Subject { get; set; } = "";

        public string? Message { get; set; } = "";

        public int? Submitted { get; set; }

        public DateTime? SubmittedDateTime { get; set; }

        public int? IsSynced { get; set; }

        public byte[]? RowVersion { get; set; }

        public string? JobTitle { get; set; } = "";

        //public string DocumentPath { get; set; } = "";

        //public string DocumentName { get; set; } = "";


        public ICollection<ApplicationCommunicationDocumentAddDTO> ApplicationCommunicationDocumentAddDTO { get; set; }

        public ApplicationCommunicationAddDTO()
        {
            ApplicationCommunicationDocumentAddDTO = new HashSet<ApplicationCommunicationDocumentAddDTO>();
        }
    }
}

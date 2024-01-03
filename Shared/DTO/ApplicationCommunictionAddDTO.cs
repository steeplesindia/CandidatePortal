
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationCommunictionAddDTO : AXModelDTO
    {
        //public long Number { get; set; }

        public long ApplicationNumber { get; set; }

        public Int16? CommunicationDirection { get; set; }

        public string? Subject { get; set; } = "";

        public string? Message { get; set; } = "";

        public int? Submitted { get; set; }

        public DateTime? SubmittedDateTime { get; set; }

        //public int IsSynced { get; set; }

        //public byte[]? RowVersion { get; set; }

        public ICollection<ApplicationCommunicationDocumentAddDTO> ApplicationCommunicationDocumentAddDTO { get; set; }

        public ApplicationCommunictionAddDTO()
        {
            ApplicationCommunicationDocumentAddDTO = new HashSet<ApplicationCommunicationDocumentAddDTO>();
        }
    }
}

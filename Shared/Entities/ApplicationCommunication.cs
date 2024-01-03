
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidatePortal.Shared.Entities
{
    public class ApplicationCommunication : AXModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }
        
        public long ApplicationNumber { get; set; }

        public Int16? CommunicationDirection { get; set; }

        public string? Subject { get; set; } = "";

        public string? Message { get; set; } = "";

        public int? Submitted { get; set; }

        public DateTime? SubmittedDateTime { get; set; }

        public int? IsSynced { get; set; } = 0;

        public byte[]? RowVersion { get; set; }

        public ICollection<ApplicationCommunicationDocument> ApplicationCommunicationDocuments { get; set; }

        public ApplicationCommunication()
        {
            ApplicationCommunicationDocuments = new HashSet<ApplicationCommunicationDocument>();
        }
    }
}

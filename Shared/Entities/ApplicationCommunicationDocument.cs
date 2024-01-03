
namespace CandidatePortal.Shared.Entities
{
    public class ApplicationCommunicationDocument : AXModel
    {
        public long Number { get; set; }

        public long ApplicationCommunicationNumber { get; set; }

        public string? DocumentPath { get; set; } = "";

        public string? DocumentName { get; set; } = "";

        public int IsSynced { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}

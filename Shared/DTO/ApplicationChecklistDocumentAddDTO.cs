
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationChecklistDocumentAddDTO : AXModelDTO
    {
        //public long Number { get; set; }

        //public long ApplicationChecklistNumber { get; set; }

        public string? DocumentPath { get; set; } = "";

        public string? DocumentName { get; set; } = "";

        //public bool IsSynced { get; set; }

        //public byte[]? RowVersion { get; set; }
    }
}

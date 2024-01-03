
namespace CandidatePortal.Shared.Entities
{
    public class ApplicationChecklistDocument : AXModel
    {
        public long Number { get; set; }

        public long ApplicationChecklistNumber { get; set; }

        public string? DocumentPath { get; set; } = "";

        public string? DocumentName { get; set; } = "";

        public int? IsSynced { get; set; }

        public byte[]? RowVersion { get; set; }

        //public ApplicationChecklist ApplicationChecklist { get; set; }
    }
}

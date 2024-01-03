
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationQuestionnaireLineAnswerDTO : AXModelDTO
    {
        public long Number { get; set; }
        public long ApplicationQuestionnaireLineErpRecId { get; set; }
        public string Text { get; set; } = "";
        public byte[]? RowVersion { get; set; }
        public decimal SequenceNumber { get; set; }
        public int? IsSynced { get; set; }
        public int? IsDeleted { get; set; }
    }
}

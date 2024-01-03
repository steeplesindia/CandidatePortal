
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationQuestionnaireLineDTO : AXModelDTO
    {
        public long Number { get; set; }
        public long ApplicationQuestionnaireErpRecId { get; set; }
        public string Text { get; set; } = "";
        public Int16 Type { get; set; }
        public long AnswerErpRecId { get; set; }
        public string Answer { get; set; } = "";
        public string Comments { get; set; } = "";
        public byte[]? RowVersion { get; set; }
        public decimal SequenceNumber { get; set; }
        public int? IsSynced { get; set; }
        public int? IsDeleted { get; set; }

        public int? AnswerNumberId { get; set; } = 0;
        public float? AnswerFloatId { get; set; } = 0;
        public DateTime? AnswerDateTimeId { get; set; } = DateTime.Today;
        public TimeSpan? AnswerTimeId { get; set; } = new TimeSpan(00, 45, 00);
        public bool AnswerCheckboxId { get; set; } = false;


        public List<ApplicationQuestionnaireLineAnswerDTO> ApplicationQuestionnaireLineAnswer { get; set; }

        public ApplicationQuestionnaireLineDTO()
        {
            ApplicationQuestionnaireLineAnswer = new List<ApplicationQuestionnaireLineAnswerDTO>();
        }
    }
}

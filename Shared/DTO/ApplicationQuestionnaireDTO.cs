
namespace CandidatePortal.Shared.DTO
{
    public class ApplicationQuestionnaireDTO : AXModelDTO
    {

        public ApplicationQuestionnaireDTO()
        {
            ApplicationQuestionnaireLine = new List<ApplicationQuestionnaireLineDTO>();
            //ApplicationQuestionnaireLineAnswer = new HashSet<ApplicationQuestionnaireLineAnswer>();
        }

        public long Number { get; set; }
        public long ApplicationNumber { get; set; }
        public int Completed { get; set; }
        public int Status { get; set; }

        public byte[]? RowVersion { get; set; }

        public int? IsSynced { get; set; }
        public int? IsDeleted { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<ApplicationQuestionnaireLineDTO> ApplicationQuestionnaireLine { get; set; }

       
    }
}

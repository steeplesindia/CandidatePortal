using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Entities
{
    public class ApplicationQuestionnaireLine : AXModel
    {
        public long Number { get; set; }
        public long ApplicationQuestionnaireErpRecId { get; set; }
        public string QuestionId { get; set; } = "";
        public string Text { get; set; } = "";
        public Int16 Type { get; set; }
        public long AnswerErpRecId { get; set; }
        public string Answer { get; set; } = "";
        public string Comments { get; set; } = "";
        public byte[]? RowVersion { get; set; }

        public decimal SequenceNumber { get; set; }

        public int? IsSynced { get; set; }
        public int? IsDeleted { get; set; }

        public ICollection<ApplicationQuestionnaireLineAnswer> ApplicationQuestionnaireLineAnswer { get; set; }

        public ApplicationQuestionnaireLine()
        {
            ApplicationQuestionnaireLineAnswer = new HashSet<ApplicationQuestionnaireLineAnswer>();
        }
    }
}

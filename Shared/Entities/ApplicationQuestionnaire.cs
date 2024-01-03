using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Entities
{
    public class ApplicationQuestionnaire : AXModel
    {
        public long Number { get; set; }
        public long ApplicationNumber { get; set; }
        public int Completed { get; set; }
        public int Status { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte[]? RowVersion { get; set; }
        public int? IsSynced { get; set; }
        public int? IsDeleted { get; set; }

        //public ICollection<ApplicationQuestionnaireLineAnswer> ApplicationQuestionnaireLineAnswer { get; set; }

        public ICollection<ApplicationQuestionnaireLine> ApplicationQuestionnaireLine { get; set; }

        public ApplicationQuestionnaire()
        {
            ApplicationQuestionnaireLine = new HashSet<ApplicationQuestionnaireLine>();
            //ApplicationQuestionnaireLineAnswer = new HashSet<ApplicationQuestionnaireLineAnswer>();
        }
    }
}

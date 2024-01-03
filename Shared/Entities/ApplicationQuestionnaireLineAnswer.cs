using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Entities
{
    public class ApplicationQuestionnaireLineAnswer : AXModel
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

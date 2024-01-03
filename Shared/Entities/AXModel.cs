using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidatePortal.Shared.Entities
{
    public class AXModel
    {
        public string? DataArea { get; set; }
        
        public long ERPRecId { get; set; }

        //[NotMapped]
        //public Boolean Error { get; set; }
        //[NotMapped]
        //public string? ErrorMessage { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidatePortal.Shared.Model
{
    public class AXModel
    {
        [MaxLength(20)]
        public string? DataArea { get; set; }
        [Required]
        public long ERPRecId { get; set; }
        [NotMapped]
        public Boolean Error { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
}

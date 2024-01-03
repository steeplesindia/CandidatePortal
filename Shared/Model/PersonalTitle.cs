using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Model
{
    public class PersonalTitle: AXModel
    {
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Model
{
    public class Job: AXModel
    {
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public string? LongDescription { get; set; }

    }
}

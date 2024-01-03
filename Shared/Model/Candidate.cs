using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidatePortal.Shared.Model
{
    public class Candidate: AXModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }
        public PersonalTitle? PersonalTitle { get; set; }
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? MiddleName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        public string? CurrentJobTitile { get; set; }
        public Education? HighestDegree { get; set; }
        public Boolean? PreviousEmployee { get; set; }
        public Country? Country { get; set; }
        public State? State { get; set; }
        public City? City { get; set; }
        [MaxLength(250)]
        public string? Street { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required]
        public string ContactNo { get; set; }
        [MaxLength(20)]
        public string? AlternateContactNo { get; set; }
        public long? ERPRecId { get; set; }

    }
}

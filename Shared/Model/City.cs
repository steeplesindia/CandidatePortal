using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Model
{
    public class City: AXModel
    {
        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public long CountryERPRecId { get; set; }
        public Country? Country { get; set; }
        public long? StateERPRecId { get; set; }
        public State? State { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Entities
{
    public class City: AXModel
    {
        public City()
        {
            Candidates = new HashSet<Candidate>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public long? StateERPRecId { get; set; }
        public long CountryERPRecId { get; set; }
        public string? CountyId { get; set; }

        public virtual State? State { get; set; }

        public virtual Country Country { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}

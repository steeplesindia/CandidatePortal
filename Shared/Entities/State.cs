using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Entities
{
    public class State: AXModel
    {
        public State()
        {
            Cities = new HashSet<City>();
            Candidates = new HashSet<Candidate>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public long CountryERPRecId { get; set; }        
        public ICollection<City> Cities { get; set; }
        public ICollection<Candidate> Candidates { get; set; }

        public virtual Country Country { get; set; }
    }
}

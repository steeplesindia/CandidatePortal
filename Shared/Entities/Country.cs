
namespace CandidatePortal.Shared.Entities
{
    public class Country: AXModel
    {
        public Country()
        {
            Candidates = new HashSet<Candidate>();
            States = new HashSet<State>();            
            Cities = new HashSet<City>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        
        public ICollection<State> States { get; set; }        
        
        public ICollection<City> Cities { get; set; }
    }
}

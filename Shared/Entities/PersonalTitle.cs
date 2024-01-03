using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Entities
{
    public class PersonalTitle: AXModel
    {
        public PersonalTitle()
        {
            Candidates = new HashSet<Candidate>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}

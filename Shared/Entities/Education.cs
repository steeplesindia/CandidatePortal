using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.Entities
{
    public class Education: AXModel
    {
        public Education()
        {
            CandidateEducation = new HashSet<CandidateEducation>();
        }

        public string Code { get; set; }        
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public ICollection<CandidateEducation> CandidateEducation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.Entities
{
    public class EducationLevel : AXModel
    {
        public EducationLevel()
        {
            Candidates = new HashSet<Candidate>();
        }

        public string Code { get; set; }
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}

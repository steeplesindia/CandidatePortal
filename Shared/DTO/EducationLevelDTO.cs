using CandidatePortal.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class EducationLevelDTO : AXModelDTO
    {
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? DataArea { get; set; }
    }
}

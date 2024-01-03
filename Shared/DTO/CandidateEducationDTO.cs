
using CandidatePortal.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateEducationDTO : CandidateEducationAddEditDTO
    {
        public EducationDTO Education { get; set; }

    }
}

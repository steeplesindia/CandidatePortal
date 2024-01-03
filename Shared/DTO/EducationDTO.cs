
namespace CandidatePortal.Shared.DTO
{
    public class EducationDTO: AXModelDTO
    {
        public EducationDTO()
        {
            CandidateEducation = new HashSet<CandidateEducationDTO>();
        }

        public string Code { get; set; }
        
        public string? Description { get; set; }

        public ICollection<CandidateEducationDTO> CandidateEducation { get; set; }
    }
}

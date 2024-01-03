using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class PersonalTitleDTO: AXModelDTO
    {
        public string Code { get; set; }        
        public string? Description { get; set; }
    }
}

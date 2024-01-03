using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class StateDTO: AXModelDTO
    {
        public StateDTO()
        {
            Cities = new HashSet<CityDTO>();
        }
        public string Code { get; set; }        
        public string? Description { get; set; }        
        public long CountryERPRecId { get; set; }        
        public ICollection<CityDTO> Cities { get; set; }
    }
}

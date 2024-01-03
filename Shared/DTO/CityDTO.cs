using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CityDTO: AXModelDTO
    {
        public string Code { get; set; }
        
        public string? Description { get; set; }
        
        public long StateERPRecId { get; set; }

        //public long CountryERPRecId { get; set; }
       
    }
}

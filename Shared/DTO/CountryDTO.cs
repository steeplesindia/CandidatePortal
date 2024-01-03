
namespace CandidatePortal.Shared.DTO
{
    public class CountryDTO: AXModelDTO
    {
        public CountryDTO()
        {
            States = new HashSet<StateDTO>();
            //Candidates = new HashSet<CandidateDTO>();
            //Cities = new HashSet<City>();
        }
        public string Code { get; set; }
        
        public string? Description { get; set; }

        public ICollection<StateDTO> States { get; set; }
       // public ICollection<CandidateDTO> Candidates { get; set; }
        //public ICollection<City> Cities { get; set; }
    }
}

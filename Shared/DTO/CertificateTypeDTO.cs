
namespace CandidatePortal.Shared.DTO
{
    public class CertificateTypeDTO: AXModelDTO
    {
        public CertificateTypeDTO()
        {
            CandidateCertificateTypes = new HashSet<CandidateCertificateTypeDTO>();
        }

        public string Code { get; set; }
        public string? Description { get; set; }

        public ICollection<CandidateCertificateTypeDTO> CandidateCertificateTypes { get; set; }
    }
}

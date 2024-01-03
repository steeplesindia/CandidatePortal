
namespace CandidatePortal.Shared.Entities
{
    public class CertificateType: AXModel
    {
        public CertificateType()
        {
            CandidateCertificateTypes = new HashSet<CandidateCertificateType>();
        }

        public string Code { get; set; }
        public string? Description { get; set; }
        public string? DataArea { get; set; }
        public ICollection<CandidateCertificateType> CandidateCertificateTypes { get; set; }
    }
}

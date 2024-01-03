
namespace CandidatePortal.Shared.Entities
{
    public class CandidateCertificateType: AXModel
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }
        public long CertificateTypeERPRecId { get; set; }        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }
        public int? RenewalRequired { get; set; }
        public int? IsSynced { get; set; }
        public int IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }
        //public CertificateType CertificateType { get; set; }
    }
}

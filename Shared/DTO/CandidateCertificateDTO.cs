
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateCertificateDTO
    {
        public long CandidateCertiId { get; set; }

        [StringLength(50, ErrorMessage = "Certificate Type can not be greater than 50 characters")]
        public string CertificateType { get; set; }

        [StringLength(256, ErrorMessage = "Description can not be greater than 256 characters")]
        public string Description { get; set; }

        [StringLength(10, ErrorMessage = "Start Date can not be greater than 10 characters")]
        public string StartDate { get; set; }

        [StringLength(10, ErrorMessage = "End Date can not be greater than 10 characters")]
        public string EndDate { get; set; }

        [StringLength(50, ErrorMessage = "Required Renewal can not be greater than 50 characters")]
        public string RequiredRenewal { get; set; }

        public bool IsSynced { get; set; }

        public long CandidateId { get; set; }
    }
}

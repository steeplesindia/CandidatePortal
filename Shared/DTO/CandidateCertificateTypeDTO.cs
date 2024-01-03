
using CandidatePortal.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateCertificateTypeDTO : CandidateCertificateTypeAddEditDTO
    {
        public CertificateTypeDTO CertificateType { get; set; }
    }
}

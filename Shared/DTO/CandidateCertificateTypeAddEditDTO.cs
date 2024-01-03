
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateCertificateTypeAddEditDTO : AXModelDTO
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }

        //[StringLength(50, ErrorMessage = "Certificate Type can not be greater than 50 characters")]
        public long? CertificateTypeERPRecId { get; set; }

        [StringLength(1024, ErrorMessage = "Notes can not be greater than 1024 characters")]
        public string? Notes { get; set; }

        //[StartDateValidator]
        //[LessThanAttribute("EndDate", ErrorMessage = "EndDate must be greater than StartDate")]
        
        //[DataType(DataType.Date)]
        //[StringLength(10, ErrorMessage = "Start Date can not be greater than 10 characters")]
        public DateTime? StartDate { get; set; }

        //[EndDateValidator]
        //[StringLength(10, ErrorMessage = "End Date can not be greater than 10 characters")]
        //[GreaterThanAttribute("StartDate", ErrorMessage = "EndDate must be greater than StartDate")]
        //[Comparison("StartDate", ComparisonType.EqualTo, "CheckOutDate must be equal to CheckInDate")]
        //[NonEqualValidation("StartDate", "Contact No. and Alternative Contact No. must be different")]
        
        //[DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        //[StringLength(50, ErrorMessage = "Required Renewal can not be greater than 50 characters")]
        public int? RenewalRequired { get; set; }
        public int? IsSynced { get; set; }

        public int IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }

        public string? Code { get; set; }

    }
}


using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class ApplicationDTO: AXModelDTO
    {
        public long Number { get; set; }

        [Required(ErrorMessage = "Candidate Number is required")]
        public long CandidateNumber { get; set; }

        [Required(ErrorMessage = "Job ERP Rec Id is required")]
        public long JobERPRecId { get; set; }

        public Int16? ApplicationStatus { get; set; }

        public DateTime? AppliedDate { get; set; }
        public byte[]? RowVersion { get; set; }

        public int? IsSynced { get; set; }// 04-06-2022
        //public Job jobs { get; set; }
    }
}

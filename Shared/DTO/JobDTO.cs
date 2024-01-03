
using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class JobDTO: AXModelDTO
    {
        public JobDTO()
        {
            Applications = new List<ApplicationDTO>();
        }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Code")]
        [StringLength(50, ErrorMessage = "Code can not be greater than 50 characters")]
        public string Code { get; set; }

        [StringLength(100, ErrorMessage = "Description can not be greater than 100 characters")]
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Long Description is required")]
        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Job Opening Date is required")]
        public DateTime? OpeningDate { get; set; }

        [Required(ErrorMessage = "Job Closing Date is required")]
        public DateTime? ClosingDate { get; set; }

        public long CandidateNumber { get; set; }

        public long JobERPRecId { get; set; }

        //Akshay 09-06-2022
        //public long Number { get; set; }
        public string? PortalLink { get; set; } = "";
        public string? Locations { get; set; } = "";
        public int? NoOfOpenings { get; set; }
        public DateTime? ApplicationDeadline { get; set; } = null;
        public Int16? Status { get; set; }
        public int? PublishedToPortal { get; set; }
        public byte[]? RowVersion { get; set; }
        public int? IsSynced { get; set; }

        public List<ApplicationDTO> Applications { get; set; }
    }
}

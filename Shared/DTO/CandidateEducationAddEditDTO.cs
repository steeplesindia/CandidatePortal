
using CandidatePortal.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateEducationAddEditDTO : AXModelDTO
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }

        [Required(ErrorMessage = "Education is required")]

        //[StringLength(50, ErrorMessage = "Education can not be greater than 50 characters")]
        public long? EducationERPRecId { get; set; }

        //[StringLength(256, ErrorMessage = "Description can not be greater than 256 characters")]
        //public string Description { get; set; }

        //[StringLength(50, ErrorMessage = "Level Of Education can not be greater than 50 characters")]
        //public string LevelOfEducation { get; set; }

        //[StringLength(10, ErrorMessage = "Start Date can not be greater than 10 characters")]
        //[Required(ErrorMessage = "Start Date is required")]

        public DateTime? StartDate { get; set; }

        //[StringLength(10, ErrorMessage = "End Date can not be greater than 10 characters")]
        public DateTime? EndDate { get; set; }

        //[MaxLength(10, ErrorMessage = "Average can not be greater than 10 characters")]
        public string? AvgGrade { get; set; }

        //[MaxLength(10, ErrorMessage = "Duration can not be greater than 10 characters")]
        public decimal? Scale { get; set; } = 0;

        public int? IsSynced { get; set; }
        public byte[]? RowVersion { get; set; }
        //public EducationDTO Education { get; set; }

        public string? Code { get; set; }


    }
}

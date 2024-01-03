using CandidatePortal.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{
    public class CandidateExperienceDTO : AXModelDTO
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }
        
        [StringLength(50, ErrorMessage = "Employer can not be greater than 50 characters")]
        public string Employer { get; set; }

        [StringLength(30, ErrorMessage = "Position can not be greater than 30 characters")]
        public string? Position { get; set; }

        [StringLength(256, ErrorMessage = "Internet Address can not be greater than 256 characters")]
        public string? EmployerURL { get; set; }

        [StringLength(15, ErrorMessage = "Telephone can not be greater than 15 characters")]
        public string? EmployerContactNo { get; set; }

        [StringLength(50, ErrorMessage = "Location can not be greater than 50 characters")]
        public string? EmployerLocation { get; set; }

        //[StringLength(10, ErrorMessage = "Start Date can not be greater than 10 characters")]
        public DateTime? StartDate { get; set; }

        //[StringLength(10, ErrorMessage = "End Date can not be greater than 10 characters")]
        public DateTime? EndDate { get; set; }

        [StringLength(1024, ErrorMessage = "Notes can not be greater than 1024 characters")]
        public string? Notes { get; set; }

        public int? IsSynced { get; set; }
        public byte[]? RowVersion { get; set; }
        //public int IsCuurentJob { get; set; }
        public string? Code { get; set; }

    }
}

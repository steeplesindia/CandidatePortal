
namespace CandidatePortal.Shared.Entities
{
    public class CandidateExperience : AXModel
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }
        public string Employer { get; set; }
        public string? Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EmployerURL { get; set; }
        public string? EmployerContactNo { get; set; }
        public string? EmployerLocation { get; set; }
        public string? Notes { get; set; }
        public int? IsSynced { get; set; }
        public int IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}

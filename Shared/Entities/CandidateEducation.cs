
namespace CandidatePortal.Shared.Entities
{
    public class CandidateEducation: AXModel
    {
        public long Number { get; set; }// 28-05-22
        public long CandidateNumber { get; set; }
        public long EducationERPRecId { get; set; }        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AvgGrade { get; set; }
        public decimal? Scale { get; set; }
        public int? IsSynced { get; set; }
        public int IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }
        //public Education Education { get; set; }
    }
}

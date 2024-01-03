namespace CandidatePortal.Shared.Model
{
    public class CandidateEducation: AXModel
    {
        public long CandidateNumber { get; set; }
        public Candidate Candidate { get; set; }
        public long EducationERPRecId { get; set; }
        public Education Education { get; set; }
    }
}

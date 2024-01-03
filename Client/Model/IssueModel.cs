using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public class IssueModel
    {
        public int NIssueId { get; set; }
        public int NFranchiseId { get; set; }
        public int NUserId { get; set; }
        public string SFranchiseName { get; set; }
        public string SUserName { get; set; }
        public string SSoftwareUrl { get; set; }
        public string SFranchiseEmailId { get; set; }
        public string SUserEmailId { get; set; }
        public string STitle { get; set; }
        public string SSteps { get; set; }
        public string SAttachment { get; set; }
        public short? SnIssueType { get; set; }
        public short? SnFor { get; set; }
        public short? SnDevStatus { get; set; }
        public short? SnUserStatus { get; set; }
        public short? SnAssignTo { get; set; }
        public int NModifyBy { get; set; }
    }
}

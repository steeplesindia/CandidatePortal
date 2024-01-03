using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public class AppliedJobSearchModel
    {
        public Int16 snStatus { get; set; }
        public Int16 snSortBy { get; set; }

        //public ApplicationStatus enumStatus { get; set; } = ApplicationStatus.Open;

        //public SortBy enumSortBy { get; set; } = SortBy.JobTitle;
    }
}

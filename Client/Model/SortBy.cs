using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public enum SortBy 
    {
        [Display(Name = "Job Title")]
        JobTitle = 0,
        [Display(Name = "Applied Date")]
        AppliedDate = 1,
        Status = 2
    }
}

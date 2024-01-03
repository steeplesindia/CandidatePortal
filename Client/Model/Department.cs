using System;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string SearchText { get; set; } = "";

    }
}

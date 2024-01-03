using System;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        //[EmailDomainValidator(AllowDomain = "radixinfosoft.com")] //,ErrorMessage = "radixinfosoft.com doamin allowed only"
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string PhotoPath { get; set; }

        public Department Department { get; set; }
    }
}

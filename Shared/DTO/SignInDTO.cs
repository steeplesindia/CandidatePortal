using CandidatePortal.Shared.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidatePortal.Shared.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Email Id is required")]
        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Email Id")]
        [StringLength(100, ErrorMessage = "Email Id can not be greater than 100 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

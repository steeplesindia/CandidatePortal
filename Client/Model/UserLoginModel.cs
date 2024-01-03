using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Client.Model
{
    public class UserLoginModel
    {
        [Required]
        public string SUserName { get; set; }

        [Required]
        public string SPassord { get; set; }
    }
}

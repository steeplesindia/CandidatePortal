

using CandidatePortal.Shared.Entities;
using CandidatePortal.Shared.Helper;
using System.ComponentModel.DataAnnotations;

namespace CandidatePortal.Shared.DTO
{   
    public class CandidateDTO : AXModelDTO
    {
        public CandidateDTO()
        {
            CandidateExperiences = new HashSet<CandidateExperienceDTO>();
            CandidateEducations = new HashSet<CandidateEducationDTO>();
            CandidateSkills = new HashSet<CandidateSkillDTO>();
            CandidateCertificateTypes = new HashSet<CandidateCertificateTypeDTO>();
            Applications = new HashSet<ApplicationDTO>();
        }

        public long Number { get; set; }

        //[Required(ErrorMessage = "User ID is required")]
        //public Guid LoginId { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Application ID")]
        //public string NApplicationId { get; set; }

        public long? PersonalTitleERPRecId { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in First Name")]
        [StringLength(50, ErrorMessage = "First Name can not be greater than 50 characters")]
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Last Name")]
        [StringLength(50, ErrorMessage = "Last Name can not be greater than 50 characters")]
        public string? MiddleName { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Last Name")]
        [StringLength(50, ErrorMessage = "Last Name can not be greater than 50 characters")]
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Current Job Title")]
        [StringLength(100, ErrorMessage = "Current Job Title can not be greater than 100 characters")]
        public string? CurrentJobTitle { get; set; }

        //[Required(ErrorMessage = "Highest Degree is required")]
        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Highest Degree")]
        //[StringLength(50, ErrorMessage = "Highest Degree can not be greater than 50 characters")]
        public long? HighestDegreeERPRecId { get; set; }

        public int? PreviousEmployee { get; set; }

        public long? CountryERPRecId { get; set; }
        public long? StateERPRecId { get; set; }
        public long? CityERPRecId { get; set; }
        public string? ZipCode { get; set; } = ""; //Akshay 09-06-2022

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Street")]
        [StringLength(100, ErrorMessage = "Street can not be greater than 100 characters")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in EmailId")]
        [StringLength(100, ErrorMessage = "EmailId can not be greater than 100 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [NonEqualValidation("AlternateContactNo", "Contact No. and Alternative Contact No. must be different")]
        [MaxLength(20, ErrorMessage = "Contact No. can not be greater than 20 characters")]
        public string? ContactNo { get; set; }

        [NonEqualValidation("ContactNo", "Contact No. and Alternative Contact No. must be different")]
        public string? AlternateContactNo { get; set; }

        [StringLength(256, ErrorMessage = "Profile Photo can not be greater than 256 characters")]
        [Required(ErrorMessage = "Profile Photo is required")]
        public string? ImageURL { get; set; }

        [StringLength(256, ErrorMessage = "Resume can not be greater than 256 characters")]
        [Required(ErrorMessage = "Resume is required")]
        public string? ResumeURL { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in FaceBook Link")]
        [StringLength(256, ErrorMessage = "FaceBook Link can not be greater than 256 characters")]
        public string? FaceBookLink { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in Twitter Link")]
        [StringLength(256, ErrorMessage = "Twitter Link can not be greater than 256 characters")]
        public string? TwitterLink { get; set; }

        [CustomWordValidation(ErrorMessage = "Reserved word is not allow in LinkedIn Link")]
        [StringLength(256, ErrorMessage = "LinkedIn Link can not be greater than 256 characters")]
        public string? LinkedinLink { get; set; }

        //[MaxLength(1, ErrorMessage = "Search Engine Policy can not be greater than 1 characters")]
        //[Range(0, 1, ErrorMessage = "Invalid Value")]
        //public Int16 SearchEnginePolicy { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Tell About Us")]
        //public string TellAboutUs { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Activities")]
        //public string Activities { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Purpose")]
        //public string Purpose { get; set; }

        [StringLength(10, ErrorMessage = "DOB length can't be more than 10 characters.")]
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Invalid date format")]
        public DateTime? Dob { get; set; }

        //[MaxLength(2, ErrorMessage = "Gender can not be greater than 2 characters")]
        [Range(0, 10, ErrorMessage = "Invalid Value")]
        public Int16? Gender { get; set; }

        //[MaxLength(1, ErrorMessage = "Gender can not be greater than 1 characters")]
        [Range(0, 5, ErrorMessage = "Invalid Value")]
        public Int16? MaritalStatus { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Ethnic Origin")]
        //[StringLength(20, ErrorMessage = "Ethnic Origin can not be greater than 20 characters")]
        //public string SEthnicOrigin { get; set; }

        //[CustomWordValidation(ErrorMessage = "Reserved word is not allow in Ethnic Origin")]
        //[StringLength(50, ErrorMessage = "Ethnic Origin can not be greater than 50 characters")]
        //public string CitizenShipCountry { get; set; }
        
        public int? Disabled { get; set; }// 04-06-2022
        public int? CanTravel { get; set; }
        public int? CanRelocate { get; set; }// 04-06-2022

        public DateTime? GeneratedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int? IsSynced { get; set; }

        public ICollection<CandidateExperienceDTO> CandidateExperiences { get; set; }
        public ICollection<CandidateEducationDTO> CandidateEducations { get; set; }
        public ICollection<CandidateSkillDTO> CandidateSkills { get; set; }
        public ICollection<CandidateCertificateTypeDTO> CandidateCertificateTypes { get; set; }
        
        public ICollection<ApplicationDTO> Applications { get; set; }

    }
}

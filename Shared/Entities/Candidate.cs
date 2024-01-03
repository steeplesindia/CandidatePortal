
namespace CandidatePortal.Shared.Entities
{
    public class Candidate: AXModel
    {
        public Candidate()
        {
            CandidateExperiences = new HashSet<CandidateExperience>();
            CandidateEducations = new HashSet<CandidateEducation>();
            CandidateSkills = new HashSet<CandidateSkill>();
            CandidateCertificateTypes = new HashSet<CandidateCertificateType>();
            Applications = new HashSet<Application>();
        }

        public long Number { get; set; }
        public long? PersonalTitleERPRecId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? CurrentJobTitle { get; set; }
        public long? HighestDegreeERPRecId { get; set; }
        public int? PreviousEmployee { get; set; }
        public long? CountryERPRecId { get; set; }
        public long? StateERPRecId { get; set; }
        public long? CityERPRecId { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; } = ""; //Akshay 09-06-2022
       // public string? Nationality { get; set; } = ""; //Akshay 09-06-2022
        public long? NationalityId { get; set; } //Akshay 09-06-2022

        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string? AlternateContactNo { get; set; }
        public string? ImageURL { get; set; }
        public string? ResumeURL { get; set; }
        public string? FaceBookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? LinkedinLink { get; set; }
        public DateTime? Dob { get; set; }        
        public Int16? Gender { get; set; }        
        public Int16? MaritalStatus { get; set; }        
        public int? Disabled { get; set; }        // 04-06-2022
        public int? CanTravel { get; set; } // 28-05-22
        public int? CanRelocate { get; set; } // 28-05-22 // 04-06-2022
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int IsSynced { get; set; }
        public byte[]? RowVersion { get; set; }
        public int IsDeleted { get; set; }

        public ICollection<CandidateExperience> CandidateExperiences { get; set; }
        public ICollection<CandidateEducation> CandidateEducations { get; set; }
        public ICollection<CandidateSkill> CandidateSkills { get; set; }
        public ICollection<CandidateCertificateType> CandidateCertificateTypes { get; set; }
        public ICollection<Application> Applications { get; set; }

        public Country Country { get; set; }

        public State State { get; set; }

        public City City { get; set; }

        public PersonalTitle PersonalTitle { get; set; }

        public virtual EducationLevel? EducationLevel { get; set; }


        //public CertificateType CertificateType { get; set; }
        //public Education Education { get; set; }
        //public Skill Skill { get; set; }

        //public SkillLevel SkillLevel { get; set; }
    }
}

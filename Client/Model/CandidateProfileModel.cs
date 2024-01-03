namespace CandidatePortal.Client.Model
{
    public class CandidateProfileModel
    {

        public CandidateProfileModel()
        {
            EmploymentList = new List<EmploymentModel>();
            EducationSummeryList = new List<EducationSummeryModel>();
            SkillModelList = new List<SkillModel>();
            CertificateModelList = new List<CertificateModel>();
        }

        //Basic Information
        public string NApplicationId { get; set; }
        public string SPersonalTitle { get; set; }
        public string SFirstName { get; set; }
        public string SMiddleName { get; set; }
        public string SLastName { get; set; }
        public string SCurrJobTitle { get; set; }
        public string SHighestDigree { get; set; }
        public string SPreviousEmployee { get; set; }
        public string SEmailId { get; set; }
        public string SContactNo { get; set; }
        public string SHomeContactNo { get; set; }
        public string SAddress1 { get; set; }
        public string SAddress2 { get; set; }
        public int NCityId { get; set; }
        public int NStateId { get; set; }
        public int NCountryId { get; set; }
        public string SZipCode { get; set; }
        public string SSecondaryAddress1 { get; set; }
        public string SSecondaryAddress2 { get; set; }
        public string NSecondaryCityId { get; set; }
        public int NSecondaryStateId { get; set; }
        public int NSecondaryCountryId { get; set; }
        public string SSecondaryZipCode { get; set; }
        public string SProfilePhoto { get; set; }
        public string SResume { get; set; }
        public string SFaceBookLink { get; set; }
        public string STwitterLink { get; set; }
        public string SLinkdinLink { get; set; }
        public Int16 SearchEnginePolicy { get; set; }
        public string STellAboutUs { get; set; }
        public string SActivities { get; set; }
        public string SPurpose { get; set; }

        //Demographic Information
        public string SDob { get; set; }
        public Int16 SnGender { get; set; }
        public int MaritalStatus { get; set; }
        public string EthnicOrigin { get; set; }
        public bool BDisabled { get; set; }
        public bool BWillTravel { get; set; }
        public bool BWillRelocated { get; set; }

        public List<EmploymentModel> EmploymentList { get; set; }
        public List<EducationSummeryModel> EducationSummeryList { get; set; }

        public List<SkillModel> SkillModelList { get; set; }
        public List<CertificateModel> CertificateModelList { get; set; }




    }

    public class EmploymentModel
    {
        int NEmployeeId { get; set; }
        public string SEmployeer { get; set; }
        public string SDesignation { get; set; }
        public string SInternetAdds { get; set; }
        public string STelephone { get; set; }
        public DateTime? SStartDate { get; set; } = DateTime.Today;
        public DateTime? SEndDate { get; set; } = DateTime.Today;
        public bool BIsCuurentJob { get; set; }
        public string SLocation { get; set; }
    }

    public class EducationSummeryModel
    {
        int NEducationId { get; set; }
        public string SEducation { get; set; }
        public string SDescription { get; set; }
        public string SLevelOfEducation { get; set; }
        public DateTime? SStartDate { get; set; } = DateTime.Today;
        public DateTime? SEndDate { get; set; } = DateTime.Today;
        public string SAverage { get; set; }
        public string SDuration { get; set; }
    }

    public class SkillModel
    {
        int NSkillId { get; set; }
        public string SLeveltype { get; set; }
        public string SLevel { get; set; }
        public DateTime? SLevelDate { get; set; } = DateTime.Today;
        public int? SYearExperiance { get; set; }
    }

    public class CertificateModel
    {
        int NCertificateId { get; set; }
        public string SCertificateType { get; set; }
        public string SDescription { get; set; }
        public DateTime? SStartDate { get; set; } = DateTime.Today;
        public DateTime? SEndDate { get; set; } = DateTime.Today;
        public string SRequiredRenewal { get; set; }
    }
}

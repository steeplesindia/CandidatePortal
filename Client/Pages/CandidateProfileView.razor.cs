using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using CandidatePortal.Shared.Helper;

namespace CandidatePortal.Client.Pages
{
    public partial class CandidateProfileView
    {
        //IMask dateMask = new DateMask("dd/MM/yyyy");

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }


        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ICandidateRepo _objRepo { get; set; }

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }

        [Inject]
        public NavigationManager navmanager { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }


        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        DialogOptions closeButton = new DialogOptions() { CloseButton = true };
        DialogOptions noHeader = new DialogOptions() { NoHeader = true };
        DialogOptions disableBackdropClick = new DialogOptions() { DisableBackdropClick = true };
        DialogOptions fullScreen = new DialogOptions() { FullScreen = true, CloseButton = true };
        DialogOptions topCenter = new DialogOptions() { Position = DialogPosition.TopCenter };


        //StartDateValidator StartDateValidator = new StartDateValidator();
        //EndDateValidator EndDateValidator = new EndDateValidator();

        //DateCompareFluentValidator dateValidator = new DateCompareFluentValidator();


        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        List<StateDTO> obj_State = new();
        List<CityDTO> obj_City = new();
        List<SkillLevelDTO> obj_SkillLevel = new();
        List<PersonalTitleDTO> obj_PersonalTitleDTO = new();
        List<EducationDTO> obj_EducationDTO = new();
        List<SkillDTO> obj_SkillDTO = new();
        List<SkillLevelDTO> obj_SkillLevelDTO = new();
        List<CertificateTypeDTO> obj_CertificateTypeDTO = new();
        List<CountryDTO> obj_CountryDTO = new();
        CandidateAddEditDTO obj_CandidateModel = new();
        CandidateAddEditDTO obj_CandidateModelDTO = new();
        CandidateExperienceDTO obj_CandidateExperiences = new();
        CandidateEducationAddEditDTO obj_CandidateEducations = new();
        CandidateSkillAddEditDTO obj_CandidateSkills = new();
        CandidateCertificateTypeAddEditDTO obj_CandidateCertificateTypes = new();
        ResponseStatusData Obj_Response = new();

        long EducationERPRecId = 0;
        string Employer = "";
        long SkillERPRecId = 0;
        long CertificateTypeERPRecId = 0;
        string SCandidateEmail = "";
        string ButtonTitle = "";
        bool IsExperianceDiv = false;
        bool IsEducationDiv = false;
        bool IsSkillDiv = false;
        bool IsCertyDiv = false;
        bool IsBasicInfoDiv = false;
        bool IsLoding = false;

        public static string BreadCumTextHome = "";
        public static string BreadCumTextprofile = "";


        // transform lower-case chars into upper-case chars
        private static char AllUpperCase(char c) => c.ToString().ToUpperInvariant()[0];


        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Candidate Profile", href: "#", icon: Icons.Filled.VerifiedUser, disabled: true)

            //new BreadcrumbItem($"{BreadCumTextHome}", href: "", icon: Icons.Material.Filled.Home),
            //new BreadcrumbItem($"{BreadCumTextprofile}", href: "#", icon: Icons.Filled.Streetview)
        };

        #region Modal
        private string modalDisplay = "none;";
        private string modalClass = "";
        private bool showBackdrop = false;
        private string modalTitle = "";

        public void Open()
        {
            modalDisplay = "block;";
            modalClass = "show";
            showBackdrop = true;
        }

        public void Close()
        {
            modalDisplay = "none";
            modalClass = "";
            showBackdrop = false;
            visible = false;
            //IsExperianceDiv = true;
            //IsEducationDiv = false;
            //IsSkillDiv = false;
            //IsCertyDiv = false;
            //IsBasicInfoDiv = false;
            ResetFormData();
        }
        #endregion


        //DateTime? _Startdate = new DateTime();
        //DateTime? _Enddate = new DateTime();




        protected override async Task OnInitializedAsync()
        {

            //BreadCumTextHome = langaugecontainer.Keys["Home"];
            //BreadCumTextprofile = langaugecontainer.Keys["Candidate Profile"];

            IsLoding = true;

            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                SCandidateEmail = SCandidateEmail.Substring(2);
                SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);

                obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());
                obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationAddEditDTO());
                obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());
                obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeAddEditDTO());

                await GetCandidateAsync();
                IsLoding = false;
            }
            else
            {
                signOut();
            }
            IsLoding = false;

            //var authState = await AuthenticationState.GetAuthenticationStateAsync();
            //var user = authState.User;

            //if (user.Identity is not null && user.Identity.IsAuthenticated)
            //{
            //    var identity = user.Identity as ClaimsIdentity;
            //    if (identity != null)
            //    {
            //        SCandidateEmail = user.Claims.FirstOrDefault(a => a.Type == "emails")?.Value ?? "";
            //        if (SCandidateEmail != null && SCandidateEmail != "")
            //        {
            //            obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());
            //            obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationAddEditDTO());
            //            obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());
            //            obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeAddEditDTO());

            //            await GetCandidateAsync();
            //            IsLoding = false;
            //        }
            //        else
            //        {
            //            obj_snackbar.Add("Please Login", Severity.Error);
            //            _nav.NavigateTo("SignIn");
            //            IsLoding = false;
            //        }
            //    }
            //}
            //else
            //{
            //    obj_snackbar.Add("Please Login", Severity.Error);
            //    _nav.NavigateTo("SignIn");
            //    IsLoding = false;
            //}

            //SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            //if (SCandidateEmail != null && SCandidateEmail != "")
            //{
            //    //_jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/downloadScript.js");

            //    obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());
            //    obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationAddEditDTO());
            //    obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());
            //    obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeAddEditDTO());

            //    await GetCandidateAsync();
            //    IsLoding = false;
            //}
            //else
            //{
            //    obj_snackbar.Add("Please Login", Severity.Error);
            //    _nav.NavigateTo("SignIn");
            //    IsLoding = false;
            //}
        }

        #region Get Events
        public async Task GetCandidateAsync()
        {
            //Obj_Response = new ResponseStatusData();

            //CandidateParameters dto = new();
            //dto.PageNumber = 1;
            //dto.PageSize = 1;

            //if (SCandidateEmail != "")
            //{
            //    SCandidateEmail = SCandidateEmail.Substring(2);
            //    SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);
            //}

            Obj_Response = await _objRepo.GetCandidateAsync(SCandidateEmail);
            if (Obj_Response.IsSuccess)
            {
                //obj_CandidateModel = new CandidateAddEditDTO();
                obj_CandidateModel = JsonConvert.DeserializeObject<CandidateAddEditDTO>(Obj_Response.Data.ToString());
            }
        }
        public async Task GetALlMaters()
        {
            IsLoding = true;
            await GetPersonalTitleAsync();
            await GetEducationAsync();
            await GetSkillsAsync();
            await GetCertificateTypesAsync();
            await GetCountriesAsync();
            IsLoding = false;
        }
        public async Task GetCountriesAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetCountriesAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_CountryDTO = new List<CountryDTO>();
                obj_CountryDTO = JsonConvert.DeserializeObject<List<CountryDTO>>(Obj_Response.Data.ToString());
            }
        }
        public async Task GetPersonalTitleAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetPersonalTitleAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_PersonalTitleDTO = new List<PersonalTitleDTO>();
                obj_PersonalTitleDTO = JsonConvert.DeserializeObject<List<PersonalTitleDTO>>(Obj_Response.Data.ToString());

                if (obj_PersonalTitleDTO.Count > 0)
                    obj_CandidateModel.PersonalTitleERPRecId = obj_PersonalTitleDTO[0].ERPRecId;
            }
        }
        public async Task GetEducationAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetEducationAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_EducationDTO = new List<EducationDTO>();
                obj_EducationDTO = JsonConvert.DeserializeObject<List<EducationDTO>>(Obj_Response.Data.ToString());
            }
        }
        public async Task GetSkillsAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetSkillsAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_SkillDTO = new List<SkillDTO>();
                obj_SkillDTO = JsonConvert.DeserializeObject<List<SkillDTO>>(Obj_Response.Data.ToString());
            }
        }
        public async Task GetSkillLevelsAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetSkillLevelsAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_SkillLevelDTO = new List<SkillLevelDTO>();
                obj_SkillLevelDTO = JsonConvert.DeserializeObject<List<SkillLevelDTO>>(Obj_Response.Data.ToString());
            }
        }
        public async Task GetCertificateTypesAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetCertificateTypesAsync();
            if (Obj_Response.IsSuccess)
            {
                //obj_CertificateTypeDTO = new List<CertificateTypeDTO>();
                obj_CertificateTypeDTO = JsonConvert.DeserializeObject<List<CertificateTypeDTO>>(Obj_Response.Data.ToString());
            }
        }
        #endregion

        #region Change Event
        private async void ChangeCountry(long? value)
        {
            //IsLoding = true;

            obj_CandidateModel.CountryERPRecId = value;

            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetStatesByCountryIdAsync(value);
            if (Obj_Response.IsSuccess)
            {
                //obj_State = new List<StateDTO>();
                obj_State = JsonConvert.DeserializeObject<List<StateDTO>>(Obj_Response.Data.ToString());

                if (obj_State.Count > 0)
                    obj_CandidateModel.StateERPRecId = obj_State[0].ERPRecId;
            }
          //  IsLoding = false;

        }
        private async void ChangeState(long? value)
        {
            IsLoding = true;

            obj_CandidateModel.StateERPRecId = value;

            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetCitiesByStateIdAsync(value);
            if (Obj_Response.IsSuccess)
            {
                //obj_City = new List<CityDTO>();
                obj_City = JsonConvert.DeserializeObject<List<CityDTO>>(Obj_Response.Data.ToString());

                if (obj_City.Count > 0)
                    obj_CandidateModel.CityERPRecId = obj_City[0].ERPRecId;
            }
            IsLoding = false;

        }
        private async void ChangeSkill(long? value, int index)
        {
            IsLoding = true;

            obj_CandidateModel.CandidateSkills[index].SkillERPRecId = Convert.ToInt64(value);

            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetSkillBySkillIdAsync(value);
            if (Obj_Response.IsSuccess)
            {
                //obj_SkillLevel = new List<SkillLevelDTO>();
                obj_SkillLevel = JsonConvert.DeserializeObject<List<SkillLevelDTO>>(Obj_Response.Data.ToString());

                if (obj_SkillLevel.Count > 0)
                    obj_CandidateModel.CandidateSkills[index].SkillLevelERPRecId = Convert.ToInt64(obj_SkillLevel[0].ERPRecId);

            }
            IsLoding = false;

        }

        private string GetGenderName(long? value)
        {
            string gender = "";
            switch (value)
            {
                case 0:
                    gender = "None";
                    break;
                case 1:
                    gender = "Male";
                    break;
                case 2:
                    gender = "Female";
                    break;
                case 3:
                    gender = "NonSpecific";
                    break;
                default:
                    gender = "None";
                    break;
            }
            return gender;
        }

        private string GetMaritalStatusName(long? value)
        {
            string gender = "";
            switch (value)
            {
                case 0:
                    gender = "None";
                    break;
                case 1:
                    gender = "Married";
                    break;
                case 2:
                    gender = "Single";
                    break;
                case 3:
                    gender = "Widowed";
                    break;
                case 4:
                    gender = "Divorced";
                    break;
                case 5:
                    gender = "Cohabiting";
                    break;
                case 6:
                    gender = "RegisteredPartnership";
                    break;
                case 7:
                    gender = "Separated";
                    break;
                case 8:
                    gender = "CivilPartnership";
                    break;
                default:
                    gender = "None";
                    break;
            }
            return gender;
        }

        private void ExperianceDateChange(DateTime? newDate)
        {
            if (obj_CandidateModelDTO.CandidateExperiences[0].StartDate != null && obj_CandidateModelDTO.CandidateExperiences[0].EndDate != null)
            {
                if (obj_CandidateModelDTO.CandidateExperiences[0].StartDate > obj_CandidateModelDTO.CandidateExperiences[0].EndDate)
                {
                    obj_CandidateModelDTO.CandidateExperiences[0].StartDate = null;
                    obj_snackbar.Add(Obj_Response.Message, Severity.Success);
                }

                if (obj_CandidateModelDTO.CandidateExperiences[0].EndDate > obj_CandidateModelDTO.CandidateExperiences[0].StartDate)
                {
                    obj_CandidateModelDTO.CandidateExperiences[0].EndDate = null;
                    obj_snackbar.Add(Obj_Response.Message, Severity.Success);
                }
            }
            else
            {
                if (obj_CandidateModelDTO.CandidateExperiences[0].StartDate != null)
                    obj_CandidateModelDTO.CandidateExperiences[0].StartDate = newDate;
                else
                    obj_CandidateModelDTO.CandidateExperiences[0].EndDate = newDate;
            }
            //}
            //else
            //{
            //    if (obj_CandidateModelDTO.CandidateExperiences[0].StartDate != null && obj_CandidateModelDTO.CandidateExperiences[0].EndDate != null)
            //    {
            //                }
            //    else { 
            //        obj_CandidateModelDTO.CandidateExperiences[0].EndDate = newDate;
            //    }
            //}
        }

        //void OnDateChangeProfExperiance(DateTime? newDate)
        //{
        //    if (newDate > DateTime.Now)
        //    {
        //        obj_CandidateModel.Dob = DateTime.Now;
        //        obj_snackbar.Add("Date is not valid!", Severity.Error);
        //    }
        //}
        #endregion

        #region Crud Opretion

        private bool visible;
        private DialogOptions dialogOptions = new() { FullWidth = true };


        public async void CandidateBasicProfile()
        {
            navmanager.NavigateTo("/candidatebasicprofile");
        }

        public async void ShowHidePopupDivision(int nDivType)
        {
            //Open();
            visible = true;
            if (nDivType == 1)
            {
                SetPopupShowHideFlag(true, false, false, false, false, langaugecontainer.Keys["Professional Experience"]);
                obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());

                if (Employer == "")
                    ButtonTitle = langaugecontainer.Keys["Add"];
                else
                    ButtonTitle = langaugecontainer.Keys["Save"];
            }
            else if (nDivType == 2)
            {
                SetPopupShowHideFlag(false, true, false, false, false, langaugecontainer.Keys["Education Summary"]);

                //obj_CandidateModelDTO.CandidateEducations = new List<CandidateEducationAddEditDTO>();
                obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationAddEditDTO());

               // IsLoding = true;
                await GetEducationAsync();
                //IsLoding = false;

                if (EducationERPRecId == 0)
                    ButtonTitle = langaugecontainer.Keys["Add"];
                else
                    ButtonTitle = langaugecontainer.Keys["Save"];

            }
            else if (nDivType == 3)
            {
                SetPopupShowHideFlag(false, false, true, false, false, langaugecontainer.Keys["Skill"]);
                //obj_CandidateModelDTO.CandidateSkills = new List<CandidateSkillAddEditDTO>();
                obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());

               // IsLoding = true;
                await GetSkillsAsync();
                await GetSkillLevelsAsync();
                //IsLoding = false;

                if (SkillERPRecId == 0)
                    ButtonTitle = langaugecontainer.Keys["Add"];
                else
                    ButtonTitle = langaugecontainer.Keys["Save"];
            }
            else if (nDivType == 4)
            {
                SetPopupShowHideFlag(false, false, false, true, false, langaugecontainer.Keys["Certificate"]);

                // obj_CandidateModelDTO.CandidateCertificateTypes = new List<CandidateCertificateTypeAddEditDTO>();
                obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeAddEditDTO());

                //IsLoding = true;
                await GetCertificateTypesAsync();
                //IsLoding = false;

                if (CertificateTypeERPRecId == 0)
                    ButtonTitle = langaugecontainer.Keys["Add"];
                else
                    ButtonTitle = langaugecontainer.Keys["Save"];
            }
            else
            {
                SetPopupShowHideFlag(false, false, false, false, true, "Candidate Basic Details");

                //IsLoding = true;
                //await GetCertificateTypesAsync();
                //IsLoding = false;

                //if (CertificateTypeERPRecId == 0)
                //    ButtonTitle = "Add";
                //else
                //    ButtonTitle = "Save";
            }
        }

        private void SetPopupShowHideFlag(bool isExperianceDiv, bool isEducationDiv, bool isSkillDiv, bool isCertyDiv, bool isBasicDiv, string sModalTitle)
        {
            IsExperianceDiv = isExperianceDiv;
            IsEducationDiv = isEducationDiv;
            IsSkillDiv = isSkillDiv;
            IsCertyDiv = isCertyDiv;
            IsBasicInfoDiv = isBasicDiv;
            modalTitle = sModalTitle;
        }

        #region Employear
        private async Task EditCandidateExp(long CandidateNumber, string EmployerId)
        {
            IsLoding = true;

            Employer = EmployerId;
            //Obj_Response = new ResponseStatusData();
            ShowHidePopupDivision(1);

            Obj_Response = await _objRepo.GetCandidateExperienceAsync(CandidateNumber, Employer);
            if (Obj_Response.IsSuccess)
            {

                obj_CandidateModelDTO = new CandidateAddEditDTO();
                obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());

                obj_CandidateModelDTO.CandidateExperiences[0] = JsonConvert.DeserializeObject<CandidateExperienceDTO>(Obj_Response.Data.ToString());
            }
            IsLoding = false;
        }

        public async Task AddEditCandidateExperienceAsync()
        {
            //IsLoding = true;

            mappingExpreince();
            //IsLoding = true;

            if ((obj_CandidateExperiences.Employer != null && obj_CandidateExperiences.Employer != "") && obj_CandidateExperiences.StartDate != null)
            {

                if (Employer != "" && Employer != "0")
                {
                    Obj_Response = await _objRepo.EditCandidateExperienceAsync(obj_CandidateExperiences);
                    if (Obj_Response.IsSuccess)
                    {
                        var itemToRemove = obj_CandidateModel.CandidateExperiences.Single(r => r.Employer == Employer && r.CandidateNumber == obj_CandidateExperiences.CandidateNumber);
                        obj_CandidateModel.CandidateExperiences.Remove(itemToRemove);

                        //obj_CandidateExperiences = new CandidateExperienceDTO();
                        obj_CandidateExperiences = JsonConvert.DeserializeObject<CandidateExperienceDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateExperiences.Add(obj_CandidateExperiences);
                        Close();
                        GetMassage();
                        ResetFormData();

                    }
                    else
                    {
                        GetMassage();
                    }
                }
                else
                {
                    Obj_Response = await _objRepo.AddCandidateExperienceAsync(obj_CandidateExperiences);
                    if (Obj_Response.IsSuccess)
                    {
                        //obj_CandidateExperiences = new CandidateExperienceDTO();
                        obj_CandidateExperiences = JsonConvert.DeserializeObject<CandidateExperienceDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateExperiences.Add(obj_CandidateExperiences);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                    else
                    {
                        GetMassage();
                    }
                }
            }
            else
            {
                if (obj_CandidateExperiences.StartDate == null)
                    obj_snackbar.Add("Start date is required!", Severity.Error);
                else
                    obj_snackbar.Add("Employer is required!", Severity.Error);
            }
            //IsLoding = false;

        }

        public void mappingExpreince()
        {
            obj_CandidateExperiences.CandidateNumber = obj_CandidateModel.Number;
            obj_CandidateExperiences.Employer = obj_CandidateModelDTO.CandidateExperiences[0].Employer;
            obj_CandidateExperiences.Position = obj_CandidateModelDTO.CandidateExperiences[0].Position;
            obj_CandidateExperiences.EmployerURL = obj_CandidateModelDTO.CandidateExperiences[0].EmployerURL;
            obj_CandidateExperiences.EmployerContactNo = obj_CandidateModelDTO.CandidateExperiences[0].EmployerContactNo;
            obj_CandidateExperiences.EmployerLocation = obj_CandidateModelDTO.CandidateExperiences[0].EmployerLocation;
            obj_CandidateExperiences.StartDate = obj_CandidateModelDTO.CandidateExperiences[0].StartDate;
            obj_CandidateExperiences.Notes = obj_CandidateModelDTO.CandidateExperiences[0].Notes;


            if (obj_CandidateModelDTO.CandidateExperiences[0].EndDate == null)
            {
                obj_CandidateExperiences.EndDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                obj_CandidateExperiences.EndDate = obj_CandidateModelDTO.CandidateExperiences[0].EndDate;
            }
        }

        public async Task DeleteCandidateExperienceAsync(long CandidateNumber, string Employer)
        {
            try
            {
                //IsLoding = true;

                //Obj_Response = new ResponseStatusData();
                Obj_Response = await _objRepo.DeleteCandidateExperienceAsync(CandidateNumber, Employer);
                if (Obj_Response.IsSuccess)
                {
                    var itemToRemove = obj_CandidateModel.CandidateExperiences.Single(r => r.Employer == Employer && r.CandidateNumber == CandidateNumber);
                    obj_CandidateModel.CandidateExperiences.Remove(itemToRemove);

                    GetMassage();
                }
                else
                {
                    GetMassage();
                }
                //IsLoding = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void OnDateChangeStart(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateExperiences[0].StartDate = newDate;
            CompaireExperianceDate();
        }
        void OnDateChangeEnd(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateExperiences[0].EndDate = newDate;
            CompaireExperianceDate();
        }
        public void CompaireExperianceDate()
        {
            if (obj_CandidateModelDTO.CandidateExperiences[0].StartDate != null && obj_CandidateModelDTO.CandidateExperiences[0].EndDate != null)
            {
                if (!(obj_CandidateModelDTO.CandidateExperiences[0].StartDate <= obj_CandidateModelDTO.CandidateExperiences[0].EndDate))
                {
                    obj_CandidateModelDTO.CandidateExperiences[0].EndDate = null;
                    obj_snackbar.Add("EndDate must be greater than StartDate", Severity.Error);
                }
            }

        }

        #endregion

        #region Candidate Education
        private async Task EditEducationValue(long CandidateNumber, long? id)
        {
            IsLoding = true;

            EducationERPRecId = Convert.ToInt64(id);
            //Obj_Response = new ResponseStatusData();
            ShowHidePopupDivision(2);

            Obj_Response = await _objRepo.GetCandidateEducationAsync(CandidateNumber, EducationERPRecId);
            if (Obj_Response.IsSuccess)
            {
                obj_CandidateModelDTO = new CandidateAddEditDTO();
                obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationDTO());

                obj_CandidateModelDTO.CandidateEducations[0] = JsonConvert.DeserializeObject<CandidateEducationDTO>(Obj_Response.Data.ToString());
            }
            IsLoding = false;

        }

        public void mappingEducation()
        {
            obj_CandidateEducations.CandidateNumber = obj_CandidateModel.Number;
            obj_CandidateEducations.EducationERPRecId = obj_CandidateModelDTO.CandidateEducations[0].EducationERPRecId;

            if (obj_CandidateModelDTO.CandidateEducations[0].EndDate == null)
            {
                obj_CandidateEducations.EndDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                obj_CandidateEducations.EndDate = obj_CandidateModelDTO.CandidateEducations[0].EndDate;
            }

            if (obj_CandidateModelDTO.CandidateEducations[0].StartDate == null)
            {
                obj_CandidateEducations.StartDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                obj_CandidateEducations.StartDate = obj_CandidateModelDTO.CandidateEducations[0].StartDate;
            }

            obj_CandidateEducations.AvgGrade = obj_CandidateModelDTO.CandidateEducations[0].AvgGrade;
            obj_CandidateEducations.Scale = obj_CandidateModelDTO.CandidateEducations[0].Scale;
        }

        public async Task AddEditCandidateEducationAsync()
        {
            //IsLoding = true;

            //Obj_Response = new ResponseStatusData();
            mappingEducation();
            if (obj_CandidateEducations.EducationERPRecId != null && obj_CandidateEducations.EducationERPRecId != 0)
            {
                if (EducationERPRecId != null && EducationERPRecId != 0)
                {
                    Obj_Response = await _objRepo.EditCandidateEducationAsync(obj_CandidateEducations);
                    if (Obj_Response.IsSuccess)
                    {
                        var itemToRemove = obj_CandidateModel.CandidateEducations.Single(r => r.EducationERPRecId == EducationERPRecId && r.CandidateNumber == obj_CandidateEducations.CandidateNumber);
                        obj_CandidateModel.CandidateEducations.Remove(itemToRemove);

                        //obj_CandidateEducations = new CandidateEducationAddEditDTO();
                        obj_CandidateEducations = JsonConvert.DeserializeObject<CandidateEducationAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateEducations.Add(obj_CandidateEducations);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                }
                else
                {
                    Obj_Response = await _objRepo.AddCandidateEducationAsync(obj_CandidateEducations);
                    if (Obj_Response.IsSuccess)
                    {
                        //obj_CandidateEducations = new CandidateEducationAddEditDTO();
                        obj_CandidateEducations = JsonConvert.DeserializeObject<CandidateEducationAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateEducations.Add(obj_CandidateEducations);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                    else
                    {
                        GetMassage();
                    }
                }
            }
            else
            {
                obj_snackbar.Add("Education is required!", Severity.Error);
            }
            //IsLoding = false;

        }

        public async Task DeleteCandidateEducationAsync(long CandidateNumber, long? EducationERPRecId)
        {
            try
            {
                //IsLoding = true;

                //Obj_Response = new ResponseStatusData();
                Obj_Response = await _objRepo.DeleteCandidateEducationAsync(CandidateNumber, Convert.ToInt64(EducationERPRecId));
                if (Obj_Response.IsSuccess)
                {
                    var itemToRemove = obj_CandidateModel.CandidateEducations.Single(r => r.EducationERPRecId == EducationERPRecId && r.CandidateNumber == CandidateNumber);
                    obj_CandidateModel.CandidateEducations.Remove(itemToRemove);
                    GetMassage();
                }
                else
                {
                    GetMassage();
                }
                //IsLoding = false;

            }
            catch (Exception ex)
            {
                IsLoding = false;
                throw ex;
            }
        }

        void OnDateChangeStartEducation(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateEducations[0].StartDate = newDate;
            CompaireExperianceDateEducation();
        }
        void OnDateChangeEndEducation(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateEducations[0].EndDate = newDate;
            CompaireExperianceDateEducation();
        }
        public void CompaireExperianceDateEducation()
        {
            if (obj_CandidateModelDTO.CandidateEducations[0].StartDate != null && obj_CandidateModelDTO.CandidateEducations[0].EndDate != null)
            {
                if (!(obj_CandidateModelDTO.CandidateEducations[0].StartDate <= obj_CandidateModelDTO.CandidateEducations[0].EndDate))
                {
                    obj_CandidateModelDTO.CandidateEducations[0].EndDate = null;
                    obj_snackbar.Add("EndDate must be greater than StartDate", Severity.Error);
                }
            }

        }


        #endregion

        #region Skill
        //List<CandidateSkillDTO> CandidateSkills = new List<CandidateSkillDTO>();
        private async Task EditSkillValue(long CandidateNumber, long? id)
        {
            IsLoding = true;

            SkillERPRecId = Convert.ToInt64(id);
            //Obj_Response = new ResponseStatusData();
            ShowHidePopupDivision(3);

            Obj_Response = await _objRepo.GetCandidateSkillAsync(CandidateNumber, SkillERPRecId);
            if (Obj_Response.IsSuccess)
            {
                obj_CandidateModelDTO = new CandidateAddEditDTO();
                obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());

                obj_CandidateModelDTO.CandidateSkills[0] = JsonConvert.DeserializeObject<CandidateSkillAddEditDTO>(Obj_Response.Data.ToString());
            }
            IsLoding = false;
        }

        public void mappingSkill()
        {
            obj_CandidateSkills.CandidateNumber = obj_CandidateModel.Number;
            obj_CandidateSkills.SkillERPRecId = obj_CandidateModelDTO.CandidateSkills[0].SkillERPRecId;
            obj_CandidateSkills.SkillLevelERPRecId = obj_CandidateModelDTO.CandidateSkills[0].SkillLevelERPRecId;
            obj_CandidateSkills.LevelDate = obj_CandidateModelDTO.CandidateSkills[0].LevelDate;
            obj_CandidateSkills.YearOfExperience = obj_CandidateModelDTO.CandidateSkills[0].YearOfExperience;
        }
        public async Task AddEditCandidateSkillAsync()
        {
            IsLoding = true;

            //Obj_Response = new ResponseStatusData();
            mappingSkill();

            if (obj_CandidateSkills.LevelDate != null && obj_CandidateSkills.SkillERPRecId != null)
            {

                if (SkillERPRecId != null && SkillERPRecId != 0)
                {
                    Obj_Response = await _objRepo.EditCandidateSkillAsync(obj_CandidateSkills);
                    if (Obj_Response.IsSuccess)
                    {
                        var itemToRemove = obj_CandidateModel.CandidateSkills.Single(r => r.SkillERPRecId == SkillERPRecId && r.CandidateNumber == obj_CandidateSkills.CandidateNumber);
                        obj_CandidateModel.CandidateSkills.Remove(itemToRemove);

                        //obj_CandidateSkills = new CandidateSkillAddEditDTO();
                        obj_CandidateSkills = JsonConvert.DeserializeObject<CandidateSkillAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateSkills.Add(obj_CandidateSkills);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                    else
                    {
                        GetMassage();
                    }
                }
                else
                {
                    Obj_Response = await _objRepo.AddCandidateSkillAsync(obj_CandidateSkills);
                    if (Obj_Response.IsSuccess)
                    {
                        //obj_CandidateSkills = new CandidateSkillAddEditDTO();
                        obj_CandidateSkills = JsonConvert.DeserializeObject<CandidateSkillAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateSkills.Add(obj_CandidateSkills);
                        Close();
                        GetMassage();
                        ResetFormData();

                    }
                    else
                    {
                        GetMassage();
                    }
                }
            }
            else
            {
                if (obj_CandidateSkills.SkillERPRecId == null)
                    obj_snackbar.Add("Skill is required!", Severity.Error);
                else
                    obj_snackbar.Add("Level date is required!", Severity.Error);
            }
            IsLoding = false;
        }

        public async Task DeleteCandidateSkillAsync(long? CandidateNumber, long? SkillERPRecId)
        {
            try
            {
                IsLoding = true;

                //Obj_Response = new ResponseStatusData();
                Obj_Response = await _objRepo.DeleteCandidateSkillAsync(Convert.ToInt64(CandidateNumber), Convert.ToInt64(SkillERPRecId));
                if (Obj_Response.IsSuccess)
                {
                    var itemToRemove = obj_CandidateModel.CandidateSkills.Single(r => r.SkillERPRecId == SkillERPRecId && r.CandidateNumber == CandidateNumber);
                    obj_CandidateModel.CandidateSkills.Remove(itemToRemove);

                    GetMassage();
                }
                else
                {
                    GetMassage();
                }
                IsLoding = false;

            }
            catch (Exception ex)
            {
                throw ex;
                IsLoding = false;

            }
        }


        #endregion

        #region Certificate
        //List<CandidateCertificateTypeDTO> CandidateCertificateTypes = new List<CandidateCertificateTypeDTO>();
        private async Task EditCertiValue(long CandidateNumber, long? id)
        {
            IsLoding = true;

            CertificateTypeERPRecId = Convert.ToInt64(id);
            //Obj_Response = new ResponseStatusData();
            ShowHidePopupDivision(4);

            Obj_Response = await _objRepo.GetCandidateCertificateAsync(CandidateNumber, CertificateTypeERPRecId);
            if (Obj_Response.IsSuccess)
            {
                obj_CandidateModelDTO = new CandidateAddEditDTO();
                obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeDTO());

                obj_CandidateModelDTO.CandidateCertificateTypes[0] = JsonConvert.DeserializeObject<CandidateCertificateTypeDTO>(Obj_Response.Data.ToString());
            }
            IsLoding = false;
        }

        public void mappingCertificate()
        {
            obj_CandidateCertificateTypes.CandidateNumber = obj_CandidateModel.Number;
            obj_CandidateCertificateTypes.CertificateTypeERPRecId = obj_CandidateModelDTO.CandidateCertificateTypes[0].CertificateTypeERPRecId;
            obj_CandidateCertificateTypes.Notes = obj_CandidateModelDTO.CandidateCertificateTypes[0].Notes;
            obj_CandidateCertificateTypes.StartDate = obj_CandidateModelDTO.CandidateCertificateTypes[0].StartDate;

            if (obj_CandidateModelDTO.CandidateCertificateTypes[0].RenewalRequired == null)
                obj_CandidateCertificateTypes.RenewalRequired = 0;//obj_CandidateModelDTO.CandidateCertificateTypes[0].RenewalRequired;
            else
                obj_CandidateCertificateTypes.RenewalRequired = obj_CandidateModelDTO.CandidateCertificateTypes[0].RenewalRequired;

            if (obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate == null)
            {
                obj_CandidateCertificateTypes.EndDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                obj_CandidateCertificateTypes.EndDate = obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate;
            }

            //if (obj_CandidateCertificateTypes.StartDate == null)
            //{
            //    obj_CandidateCertificateTypes.StartDate = Convert.ToDateTime("01/01/1900");
            //}
            //else
            //{
            //    obj_CandidateCertificateTypes.StartDate = obj_CandidateModelDTO.CandidateCertificateTypes[0].StartDate;
            //}
        }

        public async Task AddEditCandidateCertificateAsync()
        {
            //IsLoding = true;

            //Obj_Response = new ResponseStatusData();
            mappingCertificate();

            if ((obj_CandidateCertificateTypes.CertificateTypeERPRecId != null && obj_CandidateCertificateTypes.CertificateTypeERPRecId != 0) && obj_CandidateCertificateTypes.StartDate != null)
            {
                if (CertificateTypeERPRecId != 0)
                {

                    obj_CandidateCertificateTypes.IsSynced = 0;
                    obj_CandidateCertificateTypes.IsDeleted = 0;

                    Obj_Response = await _objRepo.EditCandidateCertificateAsync(obj_CandidateCertificateTypes);
                    if (Obj_Response.IsSuccess)
                    {
                        var itemToRemove = obj_CandidateModel.CandidateCertificateTypes.Single(r => r.CertificateTypeERPRecId == CertificateTypeERPRecId && r.CandidateNumber == obj_CandidateCertificateTypes.CandidateNumber);
                        obj_CandidateModel.CandidateCertificateTypes.Remove(itemToRemove);

                        //obj_CandidateCertificateTypes = new CandidateCertificateTypeAddEditDTO();
                        obj_CandidateCertificateTypes = JsonConvert.DeserializeObject<CandidateCertificateTypeAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateCertificateTypes.Add(obj_CandidateCertificateTypes);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                    else
                    {
                        GetMassage();
                    }
                }
                else
                {
                    Obj_Response = await _objRepo.AddCandidateCertificateAsync(obj_CandidateCertificateTypes);
                    if (Obj_Response.IsSuccess)
                    {
                        //obj_CandidateCertificateTypes = new CandidateCertificateTypeAddEditDTO();
                        obj_CandidateCertificateTypes = JsonConvert.DeserializeObject<CandidateCertificateTypeAddEditDTO>(Obj_Response.Data.ToString());
                        obj_CandidateModel.CandidateCertificateTypes.Add(obj_CandidateCertificateTypes);
                        Close();
                        GetMassage();
                        ResetFormData();
                    }
                    else
                    {
                        GetMassage();
                    }
                }
            }
            else
            {
                if (obj_CandidateCertificateTypes.StartDate == null)
                    obj_snackbar.Add("Start date is required!", Severity.Error);
                else
                    obj_snackbar.Add("Certificate type is required!", Severity.Error);
            }
            //IsLoding = false;

        }

        public async Task DeleteCandidateCertificateAsync(long CandidateNumber, long? CertificateTypeERPRecId)
        {
            try
            {
                //IsLoding = true;

                //Obj_Response = new ResponseStatusData();
                Obj_Response = await _objRepo.DeleteCandidateCertificateAsync(CandidateNumber, Convert.ToInt64(CertificateTypeERPRecId));
                if (Obj_Response.IsSuccess)
                {
                    var itemToRemove = obj_CandidateModel.CandidateCertificateTypes.Single(r => r.CertificateTypeERPRecId == CertificateTypeERPRecId && r.CandidateNumber == CandidateNumber);
                    obj_CandidateModel.CandidateCertificateTypes.Remove(itemToRemove);
                    GetMassage();
                }
                else
                {
                    GetMassage();
                }
                //IsLoding = false;
            }
            catch (Exception ex)
            {
                //IsLoding = false;
                throw ex;
            }
        }

        void OnDateChangeStartCertificate(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateCertificateTypes[0].StartDate = newDate;
            CompaireExperianceDateCertificate();
        }
        void OnDateChangeEndCertificate(DateTime? newDate)
        {
            obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate = newDate;
            CompaireExperianceDateCertificate();
        }
        public void CompaireExperianceDateCertificate()
        {
            if (obj_CandidateModelDTO.CandidateCertificateTypes[0].StartDate != null && obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate != null)
            {
                if (!(obj_CandidateModelDTO.CandidateCertificateTypes[0].StartDate <= obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate))
                {
                    obj_CandidateModelDTO.CandidateCertificateTypes[0].EndDate = null;
                    obj_snackbar.Add("EndDate must be greater than StartDate", Severity.Error);
                }
            }

        }


        #endregion

        #endregion
        public void GetMassage()
        {
            if (Obj_Response.Status == "Success")
                obj_snackbar.Add(Obj_Response.Message, Severity.Success);
            else if (Obj_Response.Status == "Warning")
                obj_snackbar.Add(Obj_Response.Message, Severity.Warning);
            else
                obj_snackbar.Add(Obj_Response.Message, Severity.Error);
        }

        public void ResetFormData()
        {


            obj_CandidateCertificateTypes = new CandidateCertificateTypeAddEditDTO();
            CertificateTypeERPRecId = 0;

            obj_CandidateSkills = new CandidateSkillAddEditDTO();
            SkillERPRecId = 0;

            obj_CandidateEducations = new CandidateEducationAddEditDTO();
            EducationERPRecId = 0;

            obj_CandidateExperiences = new CandidateExperienceDTO();
            Employer = "";

            IsExperianceDiv = false;
            IsEducationDiv = false;
            IsSkillDiv = false;
            IsCertyDiv = false;
            IsBasicInfoDiv = false;

            obj_CandidateModelDTO = new();

            obj_CandidateModelDTO.CandidateExperiences = new List<CandidateExperienceDTO>();
            obj_CandidateModelDTO.CandidateEducations = new List<CandidateEducationAddEditDTO>();
            obj_CandidateModelDTO.CandidateSkills = new List<CandidateSkillAddEditDTO>();
            obj_CandidateModelDTO.CandidateCertificateTypes = new List<CandidateCertificateTypeAddEditDTO>();

            obj_CandidateModelDTO.CandidateExperiences.Add(new CandidateExperienceDTO());
            obj_CandidateModelDTO.CandidateEducations.Add(new CandidateEducationAddEditDTO());
            obj_CandidateModelDTO.CandidateSkills.Add(new CandidateSkillAddEditDTO());
            obj_CandidateModelDTO.CandidateCertificateTypes.Add(new CandidateCertificateTypeAddEditDTO());
        }

        public async void signOut()
        {
            IsLoding = false;
            await SignOutManager.SetSignOutState();
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }


        protected async Task EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                ResetFormData();
                Close();
                await GetCandidateAsync();
            }
            else
            {
                ResetFormData();
                Close();
                await GetCandidateAsync();
            }
        }
    }
}

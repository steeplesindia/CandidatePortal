using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using AKSoftware.Localization.MultiLanguages;


namespace CandidatePortal.Client.Pages
{
    public partial class Candidates
    {
        //IMask dateMask = new DateMask("dd/MM/yyyy");
        #region Declaretion && Injection
        #region Injection
        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Parameter]
        public string NCandidateId { get; set; } = "";

        [Inject]
        public ICandidateRepo _objRepo { get; set; }

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [CascadingParameter]
        protected EventCallback<string> SetTitleEvent { get; set; }

        #endregion

        private bool visible = true;
        private DialogOptions dialogOptions = new() { FullScreen = true, DisableBackdropClick = true, CloseButton = false };
        private void OpenDialog()
        {
            visible = true;
        }
        private void CloseDialog()
        {
            visible = false;
        }

        bool IsValid = true;
        //[CascadingParameter]
        //public EventCallback btn_clicked { get; set; }

        #region file Upload And Download
        private ElementReference _input;

        string FileName = "";
        string FileUrl = "";

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        //private IJSObjectReference _jsModule;

        #endregion

        #region Var Declaration
        private string FileExtension = "";
        string SCandidateEmail = "";
        public CandidateAddEditDTO obj_CandidateModel = new();
        public ResponseStatusData Obj_Response = new();
        List<StateDTO> obj_State = new();
        List<CityDTO> obj_City = new();
        List<SkillLevelDTO> obj_SkillLevel = new();
        List<PersonalTitleDTO> obj_PersonalTitleDTO = new();
        List<CountryDTO> obj_CountryDTO = new();
        List<EducationDTO> obj_EducationDTO = new();
        List<EducationLevelDTO> obj_EducationLevelDTO = new();

        List<SkillDTO> obj_SkillDTO = new();
        List<SkillLevelDTO> obj_SkillLevelDTO = new();
        List<CertificateTypeDTO> obj_CertificateTypeDTO = new();

        bool IsLodingImg = false;
        bool IsLodingResume = false;
        bool IsLoding = false;

        #endregion
        #endregion

        #region On Initilize
        protected override async Task OnInitializedAsync()
        {

            OpenDialog();
            //IsLoding = true;
            InitializationForm();

            SCandidateEmail = await localStorage.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                obj_CandidateModel.Email = SCandidateEmail.Substring(2);
                obj_CandidateModel.Email = obj_CandidateModel.Email.Remove(obj_CandidateModel.Email.Length - 2);

                obj_CandidateModel.CandidateExperiences.Add(new CandidateExperienceDTO());
                obj_CandidateModel.CandidateEducations.Add(new CandidateEducationDTO());
                obj_CandidateModel.CandidateSkills.Add(new CandidateSkillDTO());
                obj_CandidateModel.CandidateCertificateTypes.Add(new CandidateCertificateTypeDTO());

                await GetAllMasters();
                //IsLoding = false;
                OpenDialog();
                StateHasChanged();
            }
            else
            {
                CloseDialog();
                signOut();
            }

            //var authState = await AuthenticationState.GetAuthenticationStateAsync();
            //var user = authState.User;
            //if (user.Identity != null && user.Identity.IsAuthenticated)
            //{
            //    var identity = user.Identity as ClaimsIdentity;
            //    if (identity != null)
            //    {
            //        obj_CandidateModel.Email = user.Claims.FirstOrDefault(a => a.Type == "emails")?.Value;
            //        //SCandidateEmail = user.Claims.Where(a => a.Type == "emails").FirstOrDefault()?.Value;

            //        if (obj_CandidateModel.Email != null && obj_CandidateModel.Email != "")
            //        {
            //            obj_CandidateModel.CandidateExperiences.Add(new CandidateExperienceDTO());
            //            obj_CandidateModel.CandidateEducations.Add(new CandidateEducationDTO());
            //            obj_CandidateModel.CandidateSkills.Add(new CandidateSkillDTO());
            //            obj_CandidateModel.CandidateCertificateTypes.Add(new CandidateCertificateTypeDTO());

            //            await GetAllMasters();
            //            IsLoding = false;
            //            StateHasChanged();
            //        }
            //        else
            //        {
            //            obj_snackbar.Add("Please Login", Severity.Error);
            //            IsLoding = false;
            //            obj_spinner.RedirectToLogin();
            //        }
            //    }
            //}
            //else
            //{
            //    obj_snackbar.Add("Please Login", Severity.Error);
            //    IsLoding = false;
            //    obj_spinner.RedirectToLogin();
            //}
        }
        public void InitializationForm()
        {
            obj_CandidateModel = new CandidateAddEditDTO
            {
                //Number = 0,
                PersonalTitleERPRecId = null,
                FirstName = "",
                MiddleName = "",
                LastName = "",
                CurrentJobTitle = "",
                HighestDegreeERPRecId = null,
                PreviousEmployee = 0,
                CountryERPRecId = null,
                StateERPRecId = null,
                CityERPRecId = null,
                //Akshay Gohel
                CountryCode = "",
                StateCode = "",
                CityCode = "",
                ZipCode = "", //Akshay 09-06-2022
                Nationality = "",
                NationalityId = null,

                Street = "",
                Email = "",
                ContactNo = "",
                AlternateContactNo = "",
                ImageURL = "",
                ResumeURL = "",
                FaceBookLink = "",
                TwitterLink = "",
                LinkedinLink = "",
                Dob = null,
                Gender = 0,
                MaritalStatus = 0,
                Disabled = 0,//14-06-2022
                CanTravel = 0,//14-06-2022
                CanRelocate = 0,//14-06-2022
                IsSynced = 0,//14-06-2022
            };
        }
        #endregion

        #region Dynamic Controls
        #region Employear
        //List<CandidateExperienceDTO> CandidateExperiences1 = new List<CandidateExperienceDTO>();
        //private List<CandidateExperienceDTO> CandidateExperiences = new List<CandidateExperienceDTO>();
        //private List<EducationModel> CandidateExperiences1 = new List<EducationModel>();
        private void AddValue() => obj_CandidateModel.CandidateExperiences.Add(new CandidateExperienceDTO());
        private void RemoveValue(int i) => obj_CandidateModel.CandidateExperiences.RemoveAt(i);
        #endregion

        #region Education
        //List<CandidateEducationDTO> CandidateEducations = new List<CandidateEducationDTO>();
        private void AddEducationValue() => obj_CandidateModel.CandidateEducations.Add(new CandidateEducationDTO());
        private void RemoveEducationValue(int i) => obj_CandidateModel.CandidateEducations.RemoveAt(i);
        #endregion

        #region Skill
        //List<CandidateSkillDTO> CandidateSkills = new List<CandidateSkillDTO>();
        private void AddSkillValue() => obj_CandidateModel.CandidateSkills.Add(new CandidateSkillDTO());
        private void RemoveSkillValue(int i) => obj_CandidateModel.CandidateSkills.RemoveAt(i);
        #endregion

        #region Certificate
        //List<CandidateCertificateTypeDTO> CandidateCertificateTypes = new List<CandidateCertificateTypeDTO>();
        private void AddCertiValue() => obj_CandidateModel.CandidateCertificateTypes.Add(new CandidateCertificateTypeDTO());
        private void RemoveCertiValue(int i) => obj_CandidateModel.CandidateCertificateTypes.RemoveAt(i);
        #endregion
        #endregion

        #region Master
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
                {
                    obj_CandidateModel.PersonalTitleERPRecId = obj_PersonalTitleDTO[0].ERPRecId;
                }
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

        public async Task GetEducationLevelAsync()
        {
            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetEducationLevelAsync();
            if (Obj_Response.IsSuccess)
            {
                obj_EducationLevelDTO = JsonConvert.DeserializeObject<List<EducationLevelDTO>>(Obj_Response.Data.ToString());
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

        //Akshay Gohel =====
        private async Task ChangeCountry(IEnumerable<string> value)
        {
            if (value != null && value.Count() >= 0)
            {
                //IsLoding = true;

                string CountryName = value.FirstOrDefault();
                obj_CandidateModel.CountryCode = CountryName;

                long? ERPRecId = GetCountryNameToCountryId(CountryName);
                obj_CandidateModel.CountryERPRecId = ERPRecId;

                await GetStatesByCountryIdAsync(CountryName);
                await GetCitiesByCountryIdAsync(CountryName);
               // IsLoding = false;
            }
            StateHasChanged();
        }

        //Get Country name to Id
        private long? GetCountryNameToCountryId(string value)
        {
            return obj_CountryDTO.Where(y => y.Code == value).Select(x => x).FirstOrDefault().ERPRecId;
        }

        //Get Country id to name
        private string GetCountryIdToCountryName(long? value)
        {
            return obj_CountryDTO.Where(y => y.ERPRecId == value).Select(x => x).FirstOrDefault().Code;
        }

        private async Task GetStatesByCountryIdAsync(string value)
        {
            IsLoding = true;


            long? ERPRecId = GetCountryNameToCountryId(value);
            obj_CandidateModel.CountryERPRecId = ERPRecId;

            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetStatesByCountryIdAsync(ERPRecId);
            if (Obj_Response.IsSuccess)
            {
                //obj_State = new List<StateDTO>();
                obj_State = JsonConvert.DeserializeObject<List<StateDTO>>(Obj_Response.Data.ToString());

                if (obj_State.Count > 0)
                {
                    obj_CandidateModel.StateERPRecId = obj_State[0].ERPRecId;
                    obj_CandidateModel.StateCode = obj_State[0].Code;
                }
            }
            IsLoding = false;
            StateHasChanged();
        }

        private async Task GetCitiesByCountryIdAsync(string value)
        {
            IsLoding = true;

            long? ERPRecId = GetCountryNameToCountryId(value);
            obj_CandidateModel.CountryERPRecId = ERPRecId;

            //Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetCitiesByCountryIdAsync(ERPRecId);
            if (Obj_Response.IsSuccess)
            {
                //obj_City = new List<CityDTO>();
                obj_City = JsonConvert.DeserializeObject<List<CityDTO>>(Obj_Response.Data.ToString());

                if (obj_City.Count > 0)
                {
                    obj_CandidateModel.CityERPRecId = obj_City[0].ERPRecId;
                    obj_CandidateModel.CityCode = obj_City[0].Code;
                }
            }
            IsLoding = false;
            StateHasChanged();
        }

        private async Task ChangeState(IEnumerable<string> value)
        {
            if (value != null && value.Count() >= 0)
            {
                IsLoding = true;
                string StateName = value.FirstOrDefault();
                obj_CandidateModel.StateCode = StateName;

                long? StateERPRecId = GetStateNameToStateId(StateName);
                obj_CandidateModel.StateERPRecId = StateERPRecId;

                //Obj_Response = new ResponseStatusData();
                Obj_Response = await _objRepo.GetCitiesByStateIdAsync(StateERPRecId);
                if (Obj_Response.IsSuccess)
                {
                    //obj_City = new List<CityDTO>();
                    obj_City = JsonConvert.DeserializeObject<List<CityDTO>>(Obj_Response.Data.ToString());

                    if (obj_City.Count > 0)
                    {
                        obj_CandidateModel.CityERPRecId = obj_City[0].ERPRecId;
                        obj_CandidateModel.CityCode = obj_City[0].Code;
                    }
                }
                IsLoding = false;
            }
            StateHasChanged();
        }

        //Get State Name To State Id
        private long? GetStateNameToStateId(string value)
        {
            return obj_State.Where(y => y.Code == value).Select(x => x).FirstOrDefault().ERPRecId;
        }

        //Get Country id to name
        private string GetStateIdToStateName(long? value)
        {
            return obj_State.Where(y => y.ERPRecId == value).Select(x => x).FirstOrDefault().Code;
        }

        //City
        private long? GetCityNameToCityId(string value)
        {
            return obj_City.Where(y => y.Code == value).Select(x => x).FirstOrDefault().ERPRecId;
        }
        //Akshay Gohel End =====================

        #endregion

        #region Upload Image, Resume And Delete
        //File Upload Image  ==============
        private async Task UploadImage(InputFileChangeEventArgs e)
        {
            IsLodingImg = true;
            //Obj_Response = new ResponseStatusData();
            var imageFiles = e.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null)
                {
                    var resizedFile = await imageFile.RequestImageFileAsync("image/png", 300, 500);
                    using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);

                        FileNameListModel model = await _objRepo.UploadImage(content);

                        obj_CandidateModel.ImageURL = model.SFilePath;
                        if (obj_CandidateModel.ImageURL != null && obj_CandidateModel.ImageURL != null)
                        {
                            obj_snackbar.Add("Photo uploaded successfully", Severity.Success);
                            IsLodingImg = false;
                        }
                        else
                        {
                            obj_snackbar.Add("Problem in upload Photo", Severity.Error);
                            obj_CandidateModel.ImageURL = "";
                            IsLodingImg = false;
                        }
                    }
                }
            }
            IsLodingImg = false;
        }

        private async void DeleteImage()
        {
            if (@obj_CandidateModel.ImageURL != null && @obj_CandidateModel.ImageURL != "")
            {
                if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to delete?")) //for '{obj_JobDTO.Code}' job
                    return;

                IsLoding = true;
                ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
                obj_ImageModel.ImageURL = @obj_CandidateModel.ImageURL;
                obj_ImageModel.FolderName = "images";
                obj_ImageModel.IsImageDocType = 0; //Image

                //string FileName = Path.GetFileName(@obj_CandidateModel.ImageURL);
                //bool data = await _objRepo.DeleteAzureBlobFile(FileName, "image");
                bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
                if (data)
                {
                    obj_snackbar.Add("Photo deleted successfully", Severity.Success);
                    @obj_CandidateModel.ImageURL = "";
                }
                else
                {
                    obj_snackbar.Add("Problem in Photo Delete", Severity.Error);
                }
                IsLoding = false;
                StateHasChanged();
            }
        }

        //private async void DeleteImage()
        //{
        //    if (@obj_CandidateModel.ImageURL != null && @obj_CandidateModel.ImageURL != "")
        //    {
        //        IsLoding = true;
        //        string FileName = Path.GetFileName(@obj_CandidateModel.ImageURL);
        //        bool data = await _objRepo.DeleteAzureBlobFile(FileName, "images");
        //        if (data)
        //        {
        //            obj_snackbar.Add("File is deleted successfully", Severity.Success);
        //            @obj_CandidateModel.ImageURL = "";
        //        }
        //        else
        //        {
        //            obj_snackbar.Add("Problem in file delete", Severity.Error);
        //        }
        //        IsLoding = false;
        //        StateHasChanged();
        //    }
        //}
        //==============

        //File Upload Resume =============
        private async Task UploadResume(InputFileChangeEventArgs e)
        {
            IsLodingResume = true;
            var imageFiles = e.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null)
                {
                    IBrowserFile imgFile = e.File;
                    string imageType = imgFile.ContentType;
                    string ext = Path.GetExtension(e.File.Name).ToLower();
                    if (ext != ".pdf")
                    {
                        obj_snackbar.Add("Only PDF file allowed", Severity.Error);
                        IsLodingResume = false;
                        return;
                    }

                    //var resizedFile = await imgFile.RequestImageFileAsync(imageType, 900, 900);
                    using var ms = e.File.OpenReadStream(e.File.Size);//resizedFile.OpenReadStream(resizedFile.Size)
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(e.File.Size)), "image", imageFile.Name);

                    FileNameListModel model = await _objRepo.UploadResume(content);
                    obj_CandidateModel.ResumeURL = model.SFilePath;
                    if (obj_CandidateModel.ResumeURL != null && obj_CandidateModel.ResumeURL != null)
                    {
                        FileExtension = Path.GetExtension(obj_CandidateModel.ResumeURL);
                        obj_snackbar.Add("Resume & Cover letter uploaded successfully", Severity.Success);
                        IsLodingResume = false;

                    }
                    else
                    {
                        FileExtension = "";
                        obj_snackbar.Add("Problem in Upload Resume & Cover Letter (Single file)", Severity.Error);
                        obj_CandidateModel.ResumeURL = "";
                        IsLodingResume = false;

                    }
                }
            }
            IsLodingResume = false;
        }
        private async void DeleteResume()
        {
            if (@obj_CandidateModel.ResumeURL != null && @obj_CandidateModel.ResumeURL != "")
            {
                if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to delete?")) //for '{obj_JobDTO.Code}' job
                    return;

                IsLoding = true;
                ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
                obj_ImageModel.ImageURL = @obj_CandidateModel.ResumeURL;
                obj_ImageModel.FolderName = "resume";
                obj_ImageModel.IsImageDocType = 1;

                bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
                if (data)
                {
                    obj_snackbar.Add("Resume & Cover letter deleted successfully", Severity.Success);
                    @obj_CandidateModel.ResumeURL = "";
                }
                else
                {
                    obj_snackbar.Add("Problem in file delete", Severity.Error);
                }
                IsLoding = false;
                StateHasChanged();
            }
        }

        //private async void DeleteResume()
        //{
        //    if (@obj_CandidateModel.ResumeURL != null && @obj_CandidateModel.ResumeURL != "")
        //    {
        //        IsLoding = true;
        //        ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
        //        obj_ImageModel.ImageURL = @obj_CandidateModel.ResumeURL;
        //        obj_ImageModel.FolderName = "resume";
        //        obj_ImageModel.IsImageDocType = 1;


        //        bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
        //        if (data)
        //        {
        //            obj_snackbar.Add("File Deleted Successfully", Severity.Success);
        //            @obj_CandidateModel.ResumeURL = "";
        //        }
        //        else
        //        {
        //            obj_snackbar.Add("Problem in File Delete", Severity.Error);
        //        }
        //        IsLoding = false;
        //        StateHasChanged();
        //    }
        //}
        //===========
        #endregion

        #region Add Edit Delete
        public async Task AddCandidateAsync()
        {
            IsLoding = true;
            //Obj_Response = new ResponseStatusData();

            IsValid = true;
            //Validation =======

            if (obj_CandidateModel.FirstName == "")
            {
                obj_snackbar.Add("Please enter first name", Severity.Error);
                return;
            }

            if (obj_CandidateModel.LastName == "")
            {
                obj_snackbar.Add("Please enter last name", Severity.Error);
                return;
            }

            if (obj_CandidateModel.ContactNo == "")
            {
                obj_snackbar.Add("Please enter contact no.", Severity.Error);
                return;
            }

            if (obj_CandidateModel.Dob == null)
            {
                obj_CandidateModel.Dob = Convert.ToDateTime("01/01/1900");
            }

            if (string.IsNullOrEmpty(obj_CandidateModel.ResumeURL))
            {
                obj_snackbar.Add("Please select Resume & Cover Letter", Severity.Error);
                return;
            }

            RemoveModlList();
            if ((IsValid))
            {
                //Akshay Gohel =====
                if (obj_CandidateModel.CountryCode != null && obj_CandidateModel.CountryCode != "")
                    obj_CandidateModel.CountryERPRecId = GetCountryNameToCountryId(obj_CandidateModel.CountryCode);
                else
                    obj_CandidateModel.CountryERPRecId = 0;

                if (obj_CandidateModel.StateCode != null && obj_CandidateModel.StateCode != "")
                    obj_CandidateModel.StateERPRecId = GetStateNameToStateId(obj_CandidateModel.StateCode);
                else
                    obj_CandidateModel.StateERPRecId = 0;

                if (obj_CandidateModel.CityCode != null && obj_CandidateModel.CityCode != "")
                    obj_CandidateModel.CityERPRecId = GetCityNameToCityId(obj_CandidateModel.CityCode);
                else
                    obj_CandidateModel.CityERPRecId = 0;

                if (obj_CandidateModel.Nationality != null && obj_CandidateModel.Nationality != "")
                    obj_CandidateModel.NationalityId = GetCountryNameToCountryId(obj_CandidateModel.Nationality);
                else
                    obj_CandidateModel.NationalityId = 0;
                //End =================


                if (NCandidateId != "" && NCandidateId != "0")
                {
                    Obj_Response = await _objRepo.EditCandidateAsync(obj_CandidateModel);
                }
                else
                {
                    Obj_Response = await _objRepo.AddCandidateAsync(obj_CandidateModel);
                }

                if (Obj_Response.Status == "Success")
                {
                    await localStorage.SetItemAsync("IsLogin", true);

                    //ObjMainLayout.GetCcandidateDetails(obj_CandidateModel.Email);
                    //await btn_clicked.InvokeAsync();

                    SetTitleEvent.InvokeAsync(obj_CandidateModel.Email);

                    StateHasChanged();
                    InitializationForm();
                    FileExtension = "";

                    string JobERPRecId = await localStorage.GetItemAsync<string>("JobERPRecId");
                    if(!string.IsNullOrEmpty(JobERPRecId))
                        _nav.NavigateTo("JobDetail/" + JobERPRecId);

                    _nav.NavigateTo("joblisting");
                }

                IsLoding = false;
                GetMassage();
            }
            else
            { 
                IsLoding = false;
                if (obj_CandidateModel.ResumeURL == null)
                {
                    obj_snackbar.Add("Please select resume", Severity.Error);
                }
            }
        }
        #endregion

        #region Common Function
        public async Task GetAllMasters()
        {
            await GetPersonalTitleAsync();
            await GetEducationLevelAsync();
            await GetEducationAsync();
            await GetSkillsAsync();
            await GetCertificateTypesAsync();
            await GetCountriesAsync();
            await GetSkillLevelsAsync();
        }
        public void RemoveModlList()
        {
            List<int> lstindex = new List<int>();
            int i = 0;

            if (obj_CandidateModel.CandidateExperiences.Count() > 0)
            {
                foreach (var item in obj_CandidateModel.CandidateExperiences)
                {
                    if (item.Employer == null)
                        lstindex.Add(i);

                    if (item.StartDate != null && item.EndDate != null)
                    {
                        if (!(item.StartDate <= item.EndDate))
                        {
                            item.EndDate = null;
                            obj_snackbar.Add("Professional Experience" + (i + 1) + " EndDate must be greater than StartDate", Severity.Success);
                            IsValid = false;
                            return;
                        }
                    }

                    if (item.EndDate == null)
                        item.EndDate = Convert.ToDateTime("01/01/1900");

                    if (item.StartDate == null)
                        item.StartDate = Convert.ToDateTime("01/01/1900");

                    i++;
                }

                obj_CandidateModel.CandidateExperiences.RemoveAll(x => lstindex.Contains(obj_CandidateModel.CandidateExperiences.IndexOf(x)));

                //foreach (var inx in lstindex)
                //{
                //    obj_CandidateModel.CandidateExperiences.Remove(obj_CandidateModel.CandidateExperiences[inx]);
                //}
            }

            if (obj_CandidateModel.CandidateEducations.Count() > 0)
            {
                lstindex = new List<int>();
                i = 0;

                foreach (var item in obj_CandidateModel.CandidateEducations)
                {
                    if (item.EducationERPRecId == null)
                        lstindex.Add(i);

                    if (item.StartDate != null && item.EndDate != null)
                    {
                        if (!(item.StartDate <= item.EndDate))
                        {
                            item.EndDate = null;
                            obj_snackbar.Add("Education Summary" + (i + 1) + " EndDate must be greater than StartDate", Severity.Success);
                            IsValid = false;
                            return;
                        }
                    }


                    if (item.StartDate == null)
                        item.StartDate = Convert.ToDateTime("01/01/1900");

                    if (item.EndDate == null)
                        item.EndDate = Convert.ToDateTime("01/01/1900");

                    i++;
                }
                obj_CandidateModel.CandidateEducations.RemoveAll(x => lstindex.Contains(obj_CandidateModel.CandidateEducations.IndexOf(x)));

                //foreach (var inx in lstindex)
                //{
                //    obj_CandidateModel.CandidateEducations.Remove(obj_CandidateModel.CandidateEducations[inx]);
                //}


                //obj_CandidateModel.CandidateEducations.Remove(obj_CandidateModel.CandidateEducations[0]);
            }

            if (obj_CandidateModel.CandidateSkills.Count() > 0)
            {
                lstindex = new List<int>();
                i = 0;
                foreach (var item in obj_CandidateModel.CandidateSkills)
                {
                    if (item.SkillERPRecId == null)
                        lstindex.Add(i);

                    if (item.LevelDate == null)
                        item.LevelDate = Convert.ToDateTime("01/01/1900");

                    i++;
                }

                obj_CandidateModel.CandidateSkills.RemoveAll(x => lstindex.Contains(obj_CandidateModel.CandidateSkills.IndexOf(x)));

                //foreach (var inx in lstindex)
                //{
                //    obj_CandidateModel.CandidateSkills.Remove(obj_CandidateModel.CandidateSkills[inx]);
                //}

                //obj_CandidateModel.CandidateSkills.Remove(obj_CandidateModel.CandidateSkills[0]);
            }

            if (obj_CandidateModel.CandidateCertificateTypes.Count() > 0)
            {
                lstindex = new List<int>();
                i = 0;
                foreach (var item in obj_CandidateModel.CandidateCertificateTypes)
                {
                    if (item.CertificateTypeERPRecId == null)
                        lstindex.Add(i);

                    if (item.StartDate != null && item.EndDate != null)
                    {
                        if (!(item.StartDate <= item.EndDate))
                        {
                            item.EndDate = null;
                            obj_snackbar.Add("Certificate" + (i + 1) + " EndDate must be greater than StartDate", Severity.Success);
                            IsValid = false;
                            return;
                        }
                    }

                    if (item.StartDate == null)
                        item.StartDate = Convert.ToDateTime("01/01/1900");

                    if (item.EndDate == null)
                        item.EndDate = Convert.ToDateTime("01/01/1900");

                    i++;
                }

                obj_CandidateModel.CandidateCertificateTypes.RemoveAll(x => lstindex.Contains(obj_CandidateModel.CandidateCertificateTypes.IndexOf(x)));

                //obj_CandidateModel.CandidateCertificateTypes.Remove(obj_CandidateModel.CandidateCertificateTypes[0]);
            }
        }
        public async Task GetCandidateAsync()
        {
            IsLoding = true;

            Obj_Response = await _objRepo.GetCandidateAsync(obj_CandidateModel.Email);
            if (Obj_Response.IsSuccess)
            {
                obj_CandidateModel = JsonConvert.DeserializeObject<CandidateAddEditDTO>(Obj_Response.Data.ToString());
            }
            IsLoding = false;
        }
        public void GetMassage()
        {
            if (Obj_Response.Status == "Success")
                obj_snackbar.Add(Obj_Response.Message, Severity.Success);
            else if (Obj_Response.Status == "Warning")
                obj_snackbar.Add(Obj_Response.Message, Severity.Warning);
            else
                obj_snackbar.Add(Obj_Response.Message, Severity.Error);
        }
        public async void signOut()
        {
            IsLoding = false;
            await SignOutManager.SetSignOutState();
            await localStorage.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }

        #endregion

        //private string modalDisplay = "none;";
        //private string modalClass = "";
        //private bool showBackdrop = false;
        //public string ImgUrl { get; set; }
        //public string PageTitle { get; set; } = "";
        //private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        //private string _authMessage;
        //private string _surnameMessage;
        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        //public void Open()
        //{
        //    modalDisplay = "block;";
        //    modalClass = "show";
        //    showBackdrop = true;
        //}

        //public void Close()
        //{
        //    modalDisplay = "none";
        //    modalClass = "";
        //    showBackdrop = false;
        //}
    }
}

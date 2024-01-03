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
using AKSoftware.Localization.MultiLanguages;

namespace CandidatePortal.Client.Componets
{
    public partial class CandiateBasicInfo
    {
        IMask dateMask = new DateMask("dd/MM/yyyy");

        [CascadingParameter]
        public EventCallback btn_clicked { get; set; }

        [CascadingParameter]
        protected EventCallback<string> SetTitleEvent { get; set; }

        #region Declaretion && Injection
        //public IMask contactNoMask = new RegexMask(@"^(\+[\d]{1,5}|0)?[7-9]\d{9}$");


        public string mac1, mac2;

        public PatternMask mask1 = new PatternMask("(###) ###-####")
        {
            MaskChars = new[] { new MaskChar('#', @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$") }
        };

        public PatternMask mask2 = new PatternMask("XX-XX-XX-XX-XX-XX")
        {
            MaskChars = new[] { new MaskChar('X', @"[0-9a-fA-F]") },
            Placeholder = '_',
            CleanDelimiters = true,
            Transformation = null,
        };
        

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Candidate Profile", href: "#", icon: Icons.Filled.VerifiedUser, disabled: true)

        };

        #region Injection
        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Parameter]
        public string SCandidateEmail { get; set; } = "";

        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

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
        #endregion

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
        //string SCandidateEmail = "";
        public CandidateAddEditDTO obj_CandidateModel = new();
        public CandidateAddEditDTO obj_CandidateModelTemp = new();

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
            IsLoding = true;
            InitializationForm();

            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                obj_CandidateModel.Email = SCandidateEmail.Substring(2);
                obj_CandidateModel.Email = obj_CandidateModel.Email.Remove(obj_CandidateModel.Email.Length - 2);

                await GetAllMasters();
                await GetCandidateAsync();

                IsLoding = false;
                StateHasChanged();
            }
            else
            {
                signOut();
            }
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
                string CountryName = value.FirstOrDefault();
                obj_CandidateModel.CountryCode = CountryName;

                long? ERPRecId = GetCountryNameToCountryId(CountryName);
                obj_CandidateModel.CountryERPRecId = ERPRecId;

                await GetStatesByCountryIdAsync(CountryName);
                await GetCitiesByCountryIdAsync(CountryName);
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
            //IsLoding = true;
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
           //IsLoding = false;
            StateHasChanged();
        }

        private async Task GetCitiesByCountryIdAsync(string value)
        {
           // IsLoding = true;

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
           //IsLoding = false;
            StateHasChanged();
        }

        private async Task ChangeState(IEnumerable<string> value)
        {
            if (value != null && value.Count() >= 0)
            {
                //IsLoding = true;
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
               //IsLoding = false;
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
           StateDTO obj_dto = obj_State.Where(y => y.ERPRecId == value).Select(x => x).FirstOrDefault();
           return obj_dto.Code;
        }

        //City
        private long? GetCityNameToCityId(string value)
        {
            return obj_City.Where(y => y.Code == value).Select(x => x).FirstOrDefault().ERPRecId;
        }

        private string GetCityIdToCityName(long? value)
        {
            return obj_City.Where(y => y.ERPRecId == value).Select(x => x).FirstOrDefault().Code;
        }
        //Akshay Gohel End =====================

        #endregion

        #region Upload Image, Resume And Delete
        //File Upload Image  ==============
        private async Task UploadImage(InputFileChangeEventArgs e)
        {
            //Obj_Response = new ResponseStatusData();

            IsLodingImg = true;
            StateHasChanged();
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

                        obj_CandidateModel.ImageURL = "";
                        FileNameListModel model = await _objRepo.UploadImage(content);

                        obj_CandidateModel.ImageURL = model.SFilePath;
                        if (obj_CandidateModel.ImageURL != null && obj_CandidateModel.ImageURL != null)
                        {
                            obj_snackbar.Add("Photo uploaded successfully", Severity.Success);
                        }
                        else
                        {
                            obj_snackbar.Add("Problem In Upload Photo", Severity.Error);
                            obj_CandidateModel.ImageURL = "";
                            IsLodingImg = false;
                        }
                    }
                }
            }
            IsLodingImg = false;
            StateHasChanged();

            //obj_spinner.Hide();
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
                    obj_snackbar.Add("Problem in File Delete", Severity.Error);
                }
               IsLoding = false;
                StateHasChanged();
            }
        }
        //==============

        //File Upload Resume =============
        private async Task UploadResume(InputFileChangeEventArgs e)
        {
            IsLodingResume = true;
            StateHasChanged();

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
                        //FileExtension = "";
                        obj_snackbar.Add("Only PDF file allowed", Severity.Error);
                        //obj_CandidateModel.ResumeURL = "";
                        IsLodingResume = false;
                        return;
                    }

                    //var resizedFile = await imgFile.RequestImageFileAsync(imageType, 900, 900);
                    using var ms = e.File.OpenReadStream(e.File.Size);//resizedFile.OpenReadStream(resizedFile.Size)
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(e.File.Size)), "image", imageFile.Name);

                    obj_CandidateModel.ResumeURL = "";

                    FileNameListModel model = await _objRepo.UploadResume(content);
                    obj_CandidateModel.ResumeURL = model.SFilePath;
                    if (obj_CandidateModel.ResumeURL != null && obj_CandidateModel.ResumeURL != null)
                    {
                        FileExtension = Path.GetExtension(obj_CandidateModel.ResumeURL);
                        obj_snackbar.Add("Resume & Cover letter uploaded successfully", Severity.Success);
                    }
                    else
                    {
                        FileExtension = "";
                        obj_snackbar.Add("Problem in upload resume & cover letter (single file)", Severity.Error);
                        obj_CandidateModel.ResumeURL = "";
                        IsLodingResume = false;

                    }
                }
            }
            IsLodingResume = false;
            StateHasChanged();

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

        //private async void DeleteFile(string sFile, string sFolder)
        //{
        //    if (!string.IsNullOrEmpty(sFile))
        //    {
        //        IsLoding = true;
        //        ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
        //        obj_ImageModel.ImageURL = sFile;
        //        obj_ImageModel.FolderName = sFolder;

        //        bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
        //        if (data)
        //        {
        //            obj_snackbar.Add("File is deleted successfully", Severity.Success);
        //            @obj_CandidateModel.ResumeURL = "";
        //        }
        //        else
        //        {
        //            obj_snackbar.Add("Problem in file delete", Severity.Error);
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
            //IsLoding = true;

            if (obj_CandidateModel.Dob == null)
            {
                obj_CandidateModel.Dob = Convert.ToDateTime("01/01/1900");
            }


            if (string.IsNullOrEmpty(obj_CandidateModel.ResumeURL))
            {
                obj_snackbar.Add("Please select Resume & Cover Letter", Severity.Error);
                return;
            }

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


            Obj_Response = await _objRepo.EditCandidateAsync(obj_CandidateModel);
            if (Obj_Response.Status == "Success")
            {
                //Akshay Gohel 12-08
                await SetTitleEvent.InvokeAsync(obj_CandidateModel.Email);

                InitializationForm();
                FileExtension = "";
                GetMassage();

                IsSelected = true;
                await OnEmployeeSelection.InvokeAsync(IsSelected);

                _nav.NavigateTo("/candidateprofileview");

            }
            else
            {
                IsSelected = false;
                await OnEmployeeSelection.InvokeAsync(IsSelected);
            }

            //IsLoding = false;
            IsLodingImg = false;
            IsLodingResume = false;

            await btn_clicked.InvokeAsync();
            StateHasChanged();
        }

        public async Task BackToProfile()
        {
            IsSelected = false;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
            _nav.NavigateTo("/candidateprofileview");
        }
        #endregion

        #region Common Function
        public async Task GetAllMasters()
        {
            await GetPersonalTitleAsync();
            await GetEducationLevelAsync();
            await GetCountriesAsync();

            //await GetEducationAsync();
            //await GetSkillsAsync();
            //await GetCertificateTypesAsync();
            //await GetSkillLevelsAsync();
        }
        public async Task GetCandidateAsync()
        {
            IsLoding = true;
            Obj_Response = await _objRepo.GetCandidateAsync(obj_CandidateModel.Email);
            if (Obj_Response.IsSuccess)
            {
                obj_CandidateModelTemp = JsonConvert.DeserializeObject<CandidateAddEditDTO>(Obj_Response.Data.ToString());
                obj_CandidateModel.CountryERPRecId = obj_CandidateModelTemp.CountryERPRecId;
                obj_CandidateModel.NationalityId = obj_CandidateModelTemp.NationalityId;

                obj_CandidateModel = obj_CandidateModelTemp;

                var CountryERPRecId = obj_CandidateModelTemp.CountryERPRecId;
                var StateERPRecId = obj_CandidateModelTemp.StateERPRecId;
                var CityERPRecId = obj_CandidateModelTemp.CityERPRecId;

                var NationalityId= obj_CandidateModelTemp.NationalityId;


                if (obj_CandidateModel.HighestDegreeERPRecId == null)
                {
                    obj_CandidateModel.HighestDegreeERPRecId = 0;
                }

                if (obj_CandidateModel.PersonalTitleERPRecId == null)
                {
                    obj_CandidateModel.PersonalTitleERPRecId = 0;
                }

                if (CountryERPRecId != null && CountryERPRecId != 0)
                {
                    //obj_CandidateModel.CountryCode = GetCountryIdToCountryName(obj_CandidateModelTemp.CountryERPRecId);
                    IEnumerable<string> stringEn = new[] { obj_CandidateModel.Country };
                    await ChangeCountry(stringEn);
                }
                else
                { 
                    obj_CandidateModel.CountryERPRecId = 0;
                    IEnumerable<string> stringEn = new[] { obj_CountryDTO[0].Code };
                    await ChangeCountry(stringEn);
                 
                    obj_CandidateModel.StateERPRecId = 0;
                    obj_CandidateModel.CityERPRecId = 0;
                }

                if (StateERPRecId != null && StateERPRecId != 0)
                {
                    //obj_CandidateModel.StateCode = GetStateIdToStateName(obj_CandidateModelTemp.StateERPRecId);
                    IEnumerable<string> stringEn = new[] { obj_CandidateModel.State };
                    await ChangeState(stringEn);
                }
                else
                {
                    obj_CandidateModel.StateERPRecId = 0;
                }

                if (CityERPRecId != null && CityERPRecId != 0)
                {
                    obj_CandidateModel.CityCode = obj_CandidateModelTemp.City;
                    obj_CandidateModel.CityERPRecId = obj_CandidateModelTemp.CityERPRecId;
                }
                else
                {
                    obj_CandidateModel.CityERPRecId = 0;
                }

                if (NationalityId != null && NationalityId != 0)
                {
                    obj_CandidateModel.Nationality = obj_CandidateModelTemp.Nationality;
                    obj_CandidateModel.NationalityId = obj_CandidateModelTemp.NationalityId;
                }
                else
                {
                    obj_CandidateModel.NationalityId = 0;
                }

                //if (CityERPRecId != null && CityERPRecId != 0)
                //{
                //    obj_CandidateModel.CityCode = obj_CandidateModelTemp.City;
                //}

                if (obj_CandidateModel.ResumeURL != null && obj_CandidateModel.ResumeURL != null)
                {
                    FileExtension = Path.GetExtension(obj_CandidateModel.ResumeURL);
                }
                StateHasChanged();
            }
           IsLoding = false;
            //GetMassage();
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
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }
        #endregion
    }
}

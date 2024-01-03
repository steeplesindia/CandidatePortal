using AKSoftware.Localization.MultiLanguages;
using Blazored.TextEditor;
using CandidatePortal.Client.Repository;
using CandidatePortal.Client.Services;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.StaticFiles;
using CandidatePortal.Client.Model;

namespace CandidatePortal.Client.Pages
{
    public partial class ApplicationCommunication
    {
        #region Declaretion
        public BlazoredTextEditor QuillHtml { get; set; } = new BlazoredTextEditor();

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Parameter]
        public string NCandidateId { get; set; } = "";

        [Parameter]
        public long ApplicationNumber { get; set; } = 0;

        [Inject]
        public IApplicationCommunication _objRepo { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }


        bool IsLoding = false;
        bool IsLodingDoc = false;

        // private IJSObjectReference _jsModule;

        static string AppsCommunicationLink = "";

        //public string ImgUrl { get; set; }
        //public string PageTitle { get; set; } = "";
        //private string lblResumeName = "";
        //private string htmltext = "";

        public ApplicationCommunicationAddDTO AppsCommModel = new ApplicationCommunicationAddDTO();

        public List<ApplicationCommunicationAddDTO> obj_AppsCommModelList = new List<ApplicationCommunicationAddDTO>();

        public ApplicationCommunicationDocumentAddDTO obj_AppsDocModel = new ApplicationCommunicationDocumentAddDTO();

        public ResponseStatusData Obj_Response = new ResponseStatusData();

        //public MarkupStringSanitized MarkupStringSanitized = new MarkupStringSanitized();


        JobAppliedDTO obj_JobAppliedDTO = new JobAppliedDTO();

        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        string SCandidateEmail = "";

        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();

        private bool IsLoaderShow = false;

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        //[Inject]
        //public NavigationManager _nav { get; set; }

        #region file Upload And Download
        private ElementReference _input;

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public SpinnerService obj_spinner { get; set; }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("appliedjob", href: "appliedjob", icon: Icons.Material.Filled.AdsClick),
            new BreadcrumbItem("Application Communication", href: null , icon: Icons.Material.Filled.AdsClick, disabled: true)
        };

        #endregion
        #endregion

        #region On Initilize
        protected override async Task OnInitializedAsync()
        {
           // _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/downloadScript.js"); //
            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
               IsLoding = true;
                IsLoaderShow = false;
                InitializationForm();
                AppsCommunicationLink = $"AppsCommunication/{ApplicationNumber}";

                await GetApplicationDetails();
                await GetApplicationJobDetails();
                IsLoding = false;
            }
            else
            {
                signOut();
            }

            //var authState = await AuthenticationState.GetAuthenticationStateAsync();
            //var user = authState.User;
            //if (user.Identity != null && user.Identity.IsAuthenticated)
            //{
            //    var identity = user.Identity as ClaimsIdentity;
            //    if (identity != null)
            //    {
            //        SCandidateEmail = user.Claims.Where(a => a.Type == "emails").FirstOrDefault()?.Value;
            //        if (SCandidateEmail != null && SCandidateEmail != "")
            //        {
            //            await GetApplicationDetails();
            //            await GetApplicationJobDetails();
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
        }
        #endregion

        public void InitializationForm()
        {
            AppsCommModel = new ApplicationCommunicationAddDTO
            {
                Number = 0,
                ApplicationNumber = 0,
                CommunicationDirection = 1,
                Subject = "",
                Message = "",
                Submitted = 0,
                SubmittedDateTime = DateTime.Now,
                IsSynced = 0,
            };
        }

        #region Change Event
        //File Upload Image And Resume
        private async Task UploadDocuments(InputFileChangeEventArgs e)
        {
            Obj_Response = new ResponseStatusData();
            IsLodingDoc = true;
            //IsLoaderShow = true;
            //StateHasChanged();

            ClearDragClass();

            var imageFiles = e.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null)
                {
                    IBrowserFile imgFile = imageFile;
                    using var ms = imgFile.OpenReadStream(imgFile.Size);//resizedFile.OpenReadStream(resizedFile.Size)
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(imgFile.Size)), "image", imgFile.Name);

                    obj_AppsDocModel = await _objRepo.UploadSingleFileWithRandomName(content);
                    if (obj_AppsDocModel.DocumentName != null && obj_AppsDocModel.DocumentName != null)
                    {
                        AppsCommModel.ApplicationCommunicationDocumentAddDTO.Add(new ApplicationCommunicationDocumentAddDTO
                        {
                            DocumentName = obj_AppsDocModel.DocumentName,
                            DocumentPath = obj_AppsDocModel.DocumentPath,
                            DocumentRandomName = obj_AppsDocModel.DocumentRandomName,
                        });
                        obj_snackbar.Add("Document is uploaded successfully", Severity.Success);
                    }
                    else
                    {
                        obj_AppsDocModel = new ApplicationCommunicationDocumentAddDTO();
                    }
                }
            }
            //IsLoaderShow = false;
            IsLodingDoc = false;
        }

        public string getFileExtension(string FileName)
        {
            return Path.GetExtension(FileName);
        }
        private async void DeleteDocumentFile(ApplicationCommunicationDocumentAddDTO Item)
        {
            if (Item.DocumentPath != null && Item.DocumentPath != "")
            {
                if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to delete?")) //for '{obj_JobDTO.Code}' job
                    return;

                IsLoding = true;
                ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
                obj_ImageModel.ImageURL = Item.DocumentPath;
                obj_ImageModel.FolderName = "communication";
                obj_ImageModel.IsImageDocType = 2; //Delete Doc in Communication

                bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
                if (data)
                {
                    obj_snackbar.Add("File is deleted successfully", Severity.Success);
                    Item.DocumentPath = "";
                    AppsCommModel.ApplicationCommunicationDocumentAddDTO.Remove(Item);
                }
                else
                {
                    obj_snackbar.Add("Problem in file delete", Severity.Error);
                }
                IsLoding = false;
                StateHasChanged();
            }
        }

        private async Task Clear()
        {
            Clearing = true;
            fileNames.Clear();
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;
        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }

        //===========
        #endregion

        #region Add Edit Delete
        public async Task AddApplicationCommunicationAsync()
        {
           IsLoding = true;
            Obj_Response = new ResponseStatusData();

            AppsCommModel.Message = await this.QuillHtml.GetHTML();
            AppsCommModel.CommunicationDirection = 1;
            AppsCommModel.IsSynced = 0;
            AppsCommModel.ApplicationNumber = ApplicationNumber;

            //AppsCommModel.ERPRecId = false;
            Obj_Response = await _objRepo.AddCommunicationAsync(AppsCommModel);

            if (Obj_Response.Status == "Success")
            {
                InitializationForm();
                Clear();
                GetMassage();
            }
            await GetApplicationDetails();
            IsLoding = false;
        }
        #endregion

        public async Task GetApplicationDetails()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetAppCommunicaionByAppsNumberAsync(ApplicationNumber);
            if (Obj_Response.IsSuccess)
            {
                obj_AppsCommModelList = new List<ApplicationCommunicationAddDTO>();
                obj_AppsCommModelList = JsonConvert.DeserializeObject<List<ApplicationCommunicationAddDTO>>(Obj_Response.Data.ToString());
            }
        }

        //Get Job Details
        public async Task GetApplicationJobDetails()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetApplicationJobDetails(ApplicationNumber);
            if (Obj_Response.IsSuccess)
            {
                obj_JobAppliedDTO = new JobAppliedDTO();
                obj_JobAppliedDTO = JsonConvert.DeserializeObject<JobAppliedDTO>(Obj_Response.Data.ToString());
            }
        }

        private async Task Download(string FileName)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("open", FileName, "_blank");

                ////Uri uri = new Uri(FileName);
                ////FileName = System.IO.Path.GetFileName(uri.LocalPath);

                ////string contentType = "";
                ////new FileExtensionContentTypeProvider().TryGetContentType(FileName, out contentType);

                //////var Obj_Response = await Http.GetByteArrayAsync($"v1.0/Upload/DownloadCommunication/{FileName}");
                ////var Obj_Response = await _objRepo.DownloadCommunication(FileName);

                //await _jsModule.InvokeVoidAsync("download", new
                //{
                //    ByteArray = Obj_Response,
                //    FileName = FileName,
                //    ContentType = contentType
                //});

            }
            catch (AggregateException ex)
            {
                //log error
                ex.ToString();
            }
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

        private string SetDirectionalStyle(Int16? nDirection)
        {
            if (nDirection == 1) { return "width:80% !important; float:right !important"; }
            else { return "width:80% !important; float:left !important"; }
        }

        public async void signOut()
        {
            IsLoding = false;
            await SignOutManager.SetSignOutState();
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }
    }
}

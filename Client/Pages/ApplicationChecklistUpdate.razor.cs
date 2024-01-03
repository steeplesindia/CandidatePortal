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

namespace CandidatePortal.Client.Pages
{
    public partial class ApplicationChecklistUpdate
    {
        #region Declaretion
        public BlazoredTextEditor QuillHtml { get; set; } = new BlazoredTextEditor();

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        [Inject]
        public NavigationManager obj_navigation { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Parameter]
        public long Number { get; set; } = 0;

        [Parameter]
        public long ApplicationNumber { get; set; } = 0;

        [Inject]
        public IApplicationChecklist _objRepo { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }

        private IJSObjectReference _jsModule;

        private ElementReference _input;
        static string AppsCommunicationLink = "";
        bool IsLoding = false;
        bool IsLodingDoc = false;


        //Dec ===========
        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Application Checklist", href: AppsCommunicationLink, icon: Icons.Material.Filled.AdsClick),
            new BreadcrumbItem("Application Checklist Update", href: null, icon: Icons.Material.Filled.AdsClick, disabled: true)
        };

        public ApplicationChecklistAddDTO AppsCommModel = new ApplicationChecklistAddDTO();
        public ApplicationChecklistAddDTO AppsCommModelData = new ApplicationChecklistAddDTO();
        public List<ApplicationChecklistAddDTO> obj_AppsCommModelList = new List<ApplicationChecklistAddDTO>();
        public ApplicationChecklistDocumentAddDTO obj_AppsDocModel = new ApplicationChecklistDocumentAddDTO();
        public ResponseStatusData Obj_Response = new ResponseStatusData();
        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

        private bool Clearing = false;
        private bool IsLoaderShow = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        private bool IsRemarks = false;
        #endregion

        #region On Initilize
        protected override async Task OnInitializedAsync()
        {

            string SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                AppsCommunicationLink = $"ApplicationChecklist/{ApplicationNumber}";
                IsLoding = true;
                InitializationForm();
                await GetApplicationDetails();
            }
            else
            {
                signOut();
            }
            IsLoding = false;
        }
        #endregion

        public void InitializationForm()
        {
            AppsCommModel = new ApplicationChecklistAddDTO
            {
                Number = 0,
                ApplicationNumber = 0,
                Subject = "",
                Description = "",
                Remarks = "",
                Status = 1,
                SubmittedDateTime = DateTime.Now,
            };
        }

        public async Task GetApplicationDetails()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetAppCommunicaionByNumber(Number);
            if (Obj_Response.IsSuccess)
            {
                AppsCommModelData = JsonConvert.DeserializeObject<ApplicationChecklistAddDTO>(Obj_Response.Data.ToString());

                AppsCommModel.Subject = AppsCommModelData.Subject;
                AppsCommModel.Description = AppsCommModelData.Description;
                AppsCommModel.Number = AppsCommModelData.Number;
                AppsCommModel.ApplicationNumber = AppsCommModelData.ApplicationNumber;
                AppsCommModel.Status = AppsCommModelData.Status;
                AppsCommModel.Remarks = AppsCommModelData.Remarks;
                AppsCommModel.Completed = AppsCommModelData.Completed;


                if (AppsCommModelData.Remarks != null && AppsCommModelData.Remarks != "")
                {
                    IsRemarks = true;
                }
                else
                {
                    IsRemarks = false;
                }
            }
        }

        #region Change Event
        //File Upload Image And Resume
        private async Task UploadDocuments(InputFileChangeEventArgs e)
        {
            Obj_Response = new ResponseStatusData();

            //IsLoaderShow = true;
            //StateHasChanged();
            IsLodingDoc = true;

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

                    obj_AppsDocModel = await _objRepo.UploadSingleFileWithRandomNameCheckList(content);
                    if (obj_AppsDocModel.DocumentName != null && obj_AppsDocModel.DocumentName != null)
                    {
                        AppsCommModel.ApplicationChecklistDocumentDTOs.Add(new ApplicationChecklistDocumentAddDTO
                        {
                            DocumentName = obj_AppsDocModel.DocumentName,
                            DocumentPath = obj_AppsDocModel.DocumentPath,
                        });
                        obj_snackbar.Add("Document is uploaded successfully", Severity.Success);
                    }
                    else
                    {
                        obj_AppsDocModel = new ApplicationChecklistDocumentAddDTO();
                    }
                }
            }
            IsLodingDoc = false;
            //IsLoaderShow = false;
            // await _jsModule.InvokeVoidAsync("hideloader");
        }

        public string getFileExtension(string FileName)
        {
            return Path.GetExtension(FileName);
        }
        private async void DeleteDocumentFile(ApplicationChecklistDocumentAddDTO Item)
        {
            if (Item.DocumentPath != null && Item.DocumentPath != "")
            {
                if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to delete?")) //for '{obj_JobDTO.Code}' job
                    return;

                IsLoding = true;
                ImageDeleteModel obj_ImageModel = new ImageDeleteModel();
                obj_ImageModel.ImageURL = Item.DocumentPath;
                obj_ImageModel.FolderName = "checklist";
                obj_ImageModel.IsImageDocType = 3; //Delete Doc in Check List 

                bool data = await _objRepo.DeleteImageFileAsync(obj_ImageModel);
                if (data)
                {
                    obj_snackbar.Add("File is deleted successfully", Severity.Success);
                    Item.DocumentPath = "";
                    AppsCommModel.ApplicationChecklistDocumentDTOs.Remove(Item);

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

        private async Task Download(string FileName)
        {
            try
            {

                await JSRuntime.InvokeVoidAsync("open", FileName, "_blank");

                // _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/downloadScript.js");

                // Uri uri = new Uri(FileName);
                // FileName = System.IO.Path.GetFileName(uri.LocalPath);

                // string contentType = "";
                // new FileExtensionContentTypeProvider().TryGetContentType(FileName, out contentType);

                // //var Obj_Response = await Http.GetByteArrayAsync($"v1.0/Upload/DownloadCommunication/{FileName}");
                // var Obj_Response = await _objRepo.Downloadchecklist(FileName);
                //// await _jsModule.InvokeVoidAsync("downloadfile", FileName, contentType, Obj_Response);

                // await _jsModule.InvokeVoidAsync("download", new
                // {
                //     FileName = FileName,
                //     ContentType = contentType,
                //     ByteArray = Obj_Response
                // });
            }
            catch (AggregateException ex)
            {
                //log error
                ex.ToString();
            }
        }

        //===========
        #endregion

        #region Edit 
        public async Task AddApplicationCommunicationAsync()
        {
            IsLoding = true;
            Obj_Response = new ResponseStatusData();

            AppsCommModel.Number = Number;
            AppsCommModel.ApplicationNumber = ApplicationNumber;

            if (AppsCommModelData.Completed == 0)
            {
                AppsCommModel.Remarks = await this.QuillHtml.GetHTML();
            }

            Obj_Response = await _objRepo.Edit(AppsCommModel);

            if (Obj_Response.Status == "Success")
            {
                InitializationForm();
                Clear();
                _nav.NavigateTo("ApplicationChecklist/" + ApplicationNumber);
            }
            IsLoding = false;
            GetMassage();
        }
        #endregion

        public void BackToCheckList()
        {
            _nav.NavigateTo("/ApplicationChecklist/" + ApplicationNumber);
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
    }
}

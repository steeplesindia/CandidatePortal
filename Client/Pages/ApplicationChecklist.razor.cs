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

namespace CandidatePortal.Client.Pages
{
    public partial class ApplicationChecklist
    {
        #region Declaretion
        
        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        //[Inject]
        //public NavigationManager obj_navigation { get; set; }

        [Parameter]
        public string NCandidateId { get; set; } = "";

        [Parameter]
        public long ApplicationNumber { get; set; } = 0;

        [Inject]
        public IApplicationChecklist _objRepo { get; set; }

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        //[Inject]
        //public NavigationManager _nav { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }


        private IJSObjectReference _jsModule;

        public List<ApplicationChecklistAddDTO> obj_AppsCommChecklist   = new List<ApplicationChecklistAddDTO>();
        public ResponseStatusData Obj_Response = new ResponseStatusData();

        private ElementReference divEditorElement;

        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        string SCandidateEmail = "";
        bool IsLoding = false;



        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Applied job", href: "appliedjob", icon: Icons.Material.Filled.AdsClick),
            new BreadcrumbItem("Application Checklist", href: "#", icon: Icons.Material.Filled.AdsClick, disabled: true)
        };
        #endregion

        #region On Initilize
        protected override async Task OnInitializedAsync()
        {
           IsLoding = true;
            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                await GetApplicationDetails();
                IsLoding = false;
            }
            else
            {
                signOut();
            }

            //var authState = await AuthenticationState.GetAuthenticationStateAsync();
            //var user = authState.User;
            //if (user.Identity.IsAuthenticated)
            //{
            //    var identity = user.Identity as ClaimsIdentity;
            //    if (identity != null)
            //    {
            //        SCandidateEmail = user.Claims.Where(a => a.Type == "emails").FirstOrDefault()?.Value;
            //        if (SCandidateEmail != null && SCandidateEmail != "")
            //        {
            //            await GetApplicationDetails();
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

        public async Task GetApplicationDetails()
        {
            Obj_Response = new ResponseStatusData();
            Obj_Response = await _objRepo.GetAppCommunicaionByAppsNumber(ApplicationNumber);
            if (Obj_Response.IsSuccess)
            {
                obj_AppsCommChecklist   = new List<ApplicationChecklistAddDTO>();
                obj_AppsCommChecklist   = JsonConvert.DeserializeObject<List<ApplicationChecklistAddDTO>>(Obj_Response.Data.ToString());

                //foreach (var lstitem in obj_AppsCommChecklist)
                //{
                //    divEditorElement = lstitem.Remarks;
                //    lstitem.Remarks = await JSRuntime.InvokeAsync<string>("QuillFunctions.getQuillText", divEditorElement);
                //}
            }
        }

        public async Task EditChecklist(long Number)
        {
           IsLoding = true;
            _nav.NavigateTo("ApplicationChecklistUpdate/" + Number + "/" + ApplicationNumber);
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
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }
    }
}

using CandidatePortal.Client.Model;
using CandidatePortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;

//using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Newtonsoft.Json;
using CandidatePortal.Shared.Entities;

namespace CandidatePortal.Client.Shared
{
    public partial class MainLayout
    {
        private bool _rightToLeft = false;

        //string country = "en-US";
        //bool dense = true;
        bool preserveOpenState = true;
        Breakpoint breakpoint = Breakpoint.Lg;
        DrawerClipMode clipMode = DrawerClipMode.Never;
        public LanguageDTO lanmodel = new();
        bool open = false;
        string _SearchText = "";

        [Inject]
        public NavigationManager navmanager { get; set; }

        #region Declaration
        [Inject]
        public Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [Inject]
        public SpinnerService obj_spinner { get; set; }

        //[CascadingParameter]
        //public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }

        [Inject]
        public ICandidateRepo _objRepo { get; set; }

        //EventCallback UpdateStyle => EventCallback.Factory.Create(this, GetCandidateAsync);
        //public EventCallback<string> SetTitleEvent => EventCallback.Factory.Create<string>(this, SetTitle);
        //public EventCallback<string> OnEmployeeSelection => EventCallback.Factory.Create<string>(this, SetTitle);
        EventCallback btn_clicked => EventCallback.Factory.Create(this, GetCandidateAsync);
       
        ResponseStatusData Obj_Response = new();
        CandidateDetails obj_CandidateModel = new();

        //[Parameter]
        //public EventCallback<string> SetTitleEvent => EventCallback.Factory.Create<string>(this, SetTitle);
        
        
        //[Inject]
        //public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public EventCallback<string> SetTitleEvent => EventCallback.Factory.Create<string>(this, GetCandidate);


        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }
        #endregion

        private bool _isLightMode = true;
        private bool _isAuthorized = false;

        private MudTheme _currentTheme = new MudTheme();
        public string drpvalue = "";
        string SCandidateEmail = "";

        string Title123 = "";
        void SetTitle(string t)
        {
            SCandidateEmail = t;
            GetCandidateAsync();
        }

       public async Task GetCandidate(string t)
        {
            SCandidateEmail = t;
           await GetCandidateAsync();
            StateHasChanged();
        }


        void HandleIntervalElapsed(string debouncedText)
        {
            // at this stage, interval has elapsed
        }
        
       

        void ToggleDrawer()
        {
            open = !open;
        }

 

        protected override async Task OnInitializedAsync()
        {

            _rightToLeft = false;
            _currentTheme = LightTheme();
            lanmodel.LanguageName = "en-US";
            //return base.OnInitializedAsync();
            obj_spinner.Show();


            var isLogin = await localStore.GetItemAsync<string>("IsLogin");
            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if ((SCandidateEmail != null && SCandidateEmail != "") && (isLogin != null && isLogin != "false"))
            {
                _isAuthorized = true;
                SCandidateEmail = SCandidateEmail.Substring(2);
                SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);
                await GetCandidateAsync();
                //obj_spinner.Hide();
                //navmanager.NavigateTo("/");
            }
            //else
            //{
            //    await SignOutManager.SetSignOutState();
            //    await localStorage.ClearAsync();
            //    //signOut();
            //}
            obj_spinner.Hide();
        }

        public async void signOut()
        {
            obj_spinner.Hide();
            _isAuthorized = false;
            await SignOutManager.SetSignOutState();
            await localStorage.ClearAsync();
            navmanager.NavigateTo("authentication/login");
            //navmanager.NavigateTo("/");
        }

        public async Task GetCandidateAsync()
        {
            obj_CandidateModel = await _objRepo.GetCandidateDetailsByEmailIdNew(SCandidateEmail);
            _isAuthorized = true;
            //await btn_clicked.InvokeAsync();
            //await InvokeAsync(StateHasChanged);
            //StateHasChanged();
            //if (Obj_Response.IsSuccess)
            //{
            //    //obj_CandidateModel = new CandidateAddEditDTO();
            //    obj_CandidateModel = JsonConvert.DeserializeObject<CandidateAddEditDTO>(Obj_Response.Data.ToString());
            //}
        }

        public async void ProfileView()
        {
            navmanager.NavigateTo("/candidateprofileview");
        }

        private void ToggleTheme()
        {
            _isLightMode = !_isLightMode;
            if (!_isLightMode)
            {
                _currentTheme = GenerateDarkTheme();
            }
            else
            {
                _currentTheme = LightTheme();
            }
        }
        private MudTheme GenerateDarkTheme() =>
            new MudTheme
            {
                Palette = new Palette()
                {
                    Black = "#27272f",
                    Background = "#32333d",
                    BackgroundGrey = "#27272f",
                    Surface = "#373740",
                    TextPrimary = "#ffffffb3",
                    TextSecondary = "rgba(255,255,255, 0.50)",

                },
                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Sora", "Poppins", "Helvetica", "Arial", "sans-serif" }
                    }
                }
            };

        private MudTheme LightTheme() =>
            new MudTheme
            {
                Palette = new Palette()
                {
                    Divider = "#373740"
                },

                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Sora", "Poppins", "Helvetica", "Arial", "sans-serif" }
                    }
                }
            };

        private void OnValueChanged(string value)
        {
            langaugecontainer.SetLanguage(System.Globalization.CultureInfo.GetCultureInfo(value));
            lanmodel.LanguageName = value;

            if (value == "ar-AE")
            {
                _rightToLeft = true;
            }
            else
            {
                _rightToLeft = false;
            }
            //// Assign the selected value to the Model
            //comment.Country = value;
        }

        private void SearchJob()
        {
            if (_SearchText != "")
            {
                var query = new Dictionary<string, string>
                {
                    { "searchtext", _SearchText },
                };
                navmanager.NavigateTo(QueryHelpers.AddQueryString("/joblisting", query));
                _SearchText = "";
            }
        }
    }
}

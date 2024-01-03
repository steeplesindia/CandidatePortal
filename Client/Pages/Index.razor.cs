using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;

namespace CandidatePortal.Client.Pages
{
    public partial class Index
    {
        //private WeatherForecast[]? forecasts;
        //private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        //private string _authMessage;
        //private string _surnameMessage;
        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }
        //[Inject]
        //public AuthenticationState AuthenticationState { get; set; }

        #region Declaration
        [Inject]
        public Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [Inject]
        public SpinnerService obj_spinner { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [CascadingParameter]
        protected EventCallback<string> SetTitleEvent { get; set; }

        //[Inject]
        //public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        public ICandidateRepo _objRepo { get; set; }

        string SCandidateEmail = "";
        ResponseStatusData Obj_Response = new();

        //private IEnumerable<Claim> claims = Enumerable.Empty<Claim>();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //obj_spinner.Show();
                var sClains = new List<Tuple<string, string>>();
                var authState = await authenticationStateTask;
                if (authState.User.Identity.IsAuthenticated)
                {
                    var identity = authState.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        SCandidateEmail = authState.User.Claims.Where(a => a.Type == "emails").FirstOrDefault()?.Value;

                        localStorage.SetItemAsync("Email", authState.User.Claims.Where(a => a.Type == "emails").FirstOrDefault()?.Value);
                        localStorage.SetItemAsync("Name", authState.User.Claims.Where(a => a.Type == "name").FirstOrDefault()?.Value);
                        localStorage.SetItemAsync("AuthType", authState.User.Claims.Where(a => a.Type == "idp").FirstOrDefault()?.Value);
                        localStorage.SetItemAsync("IsLogin", false);

                        SCandidateEmail = SCandidateEmail.Substring(2);
                        SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);

                        //Akshay Gohel 05-08
                        SetTitleEvent.InvokeAsync(SCandidateEmail);

                        string isUserApplyJob = await localStorage.GetItemAsync<string>("isUserApplyJob");
                        if ((isUserApplyJob != null && isUserApplyJob == "true") || !string.IsNullOrEmpty(SCandidateEmail))
                        {
                            if (await CheckCandidateEmailAvailability())
                            {

                                string JobERPRecId = await localStorage.GetItemAsync<string>("JobERPRecId");
                                if(!string.IsNullOrEmpty(JobERPRecId))
                                    Navigation.NavigateTo("JobDetail/" + JobERPRecId);
                                else
                                    Navigation.NavigateTo("joblisting");
                            }
                            else
                            {
                                Navigation.NavigateTo("candidateprofile",true);
                            }
                        }
                        else
                        {
                           Navigation.NavigateTo("joblisting");
                        }


                        //if (await CheckCandidateEmailAvailability())
                        //{
                        //    await localStorage.SetItemAsync("IsLogin", true);
                        //    //await btn_clicked.InvokeAsync();

                        //    //obj_spinner.Hide();
                        //    Navigation.NavigateTo("joblisting");
                        //}
                        //else
                        //{
                        //    //obj_spinner.Hide();
                        //    Navigation.NavigateTo("candidateprofile",true);

                        //}
                    }
                    else
                    {
                        Navigation.NavigateTo("joblisting");
                        // signOut();
                    }
                }
                else
                {
                    Navigation.NavigateTo("joblisting");
                    //signOut();
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        public async Task<bool> CheckCandidateEmailAvailability()
        {
           return await _objRepo.CheckCandidateEmailAvailability(SCandidateEmail);
        }

        public async void signOut()
        {
            obj_spinner.Hide();
            await SignOutManager.SetSignOutState();
            await localStorage.ClearAsync();
            Navigation.NavigateTo("/authentication/logout");
        }
    }
}

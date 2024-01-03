using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace CandidatePortal.Client.Pages
{
    public partial class JobDetails : ComponentBase
    {

        [Parameter]
        public long JobERPRecId { get; set; }
        
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        //[Parameter]
        //public long ApplyButton { get; set; }


        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        public ApplicationDTO obj_Application = new ApplicationDTO();

        public ResponseStatusData Obj_Response = new ResponseStatusData();

        [Inject]
        public IJobRepo _objRepo { get; set; }

        [Inject]
        public ICandidateRepo _objRepoCan { get; set; }

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        //public CandidateAddEditDTO obj_CandidateModel = new CandidateAddEditDTO();
        public JobDTO obj_JobDTO = new JobDTO();

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }

        bool IsLoding = false;


        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        private string _authMessage;
        private string _surnameMessage;
        string SCandidateEmail = "";
        string JobApply = "Apply";

        [Inject]
        public Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Job Listing", href: "/joblisting", icon: Icons.Material.Filled.AdsClick),
            new BreadcrumbItem("Job Details", href: null, icon: Icons.Filled.TableView, disabled: true)
        };

        protected override async Task OnInitializedAsync()
        {
            IsLoding = true;

            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                SCandidateEmail = SCandidateEmail.Substring(2);
                SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);

                await GetJobDetailsAsync();

                //Akshay Gohel 05-08
                string isUserApplyJob = await localStorage.GetItemAsync<string>("isUserApplyJob");
                if (isUserApplyJob == "true")
                {
                    localStorage.SetItemAsync("isUserApplyJob", false);
                    AddToApplyJobApplication();
                }
                IsLoding = false;
            }
            else
            {
                await GetJobDetailsByJobId();
                IsLoding = false;
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
            //            await GetJobDetailsAsync();
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
            IsLoding = false;
        }

        
        //Get Job Details
        public async Task GetJobDetailsAsync()
        {
            Obj_Response = new ResponseStatusData();
            //if (SCandidateEmail != "")
            //{
            //    SCandidateEmail = SCandidateEmail.Substring(2);
            //    SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);
            //}

            Obj_Response = await _objRepo.GetJobDetailsById(JobERPRecId, SCandidateEmail);
            if (Obj_Response.IsSuccess)
            {
                obj_JobDTO = JsonConvert.DeserializeObject<JobDTO>(Obj_Response.Data.ToString());
                if (obj_JobDTO.Applications.Count() > 0)
                {
                    JobApply = "Applied";
                    StateHasChanged();
                }
            }
        }


        public async Task GetJobDetailsByJobId()
        {
            Obj_Response = new ResponseStatusData();

            Obj_Response = await _objRepo.GetJobDetailsByJobId(JobERPRecId);
            if (Obj_Response.IsSuccess)
            {
                obj_JobDTO = JsonConvert.DeserializeObject<JobDTO>(Obj_Response.Data.ToString());
            }
        }


        //Add Job Aaplication Details 
        public async Task AddToApplyJobApplication()
        {
            try
            {

                //Akshay Gohel 05-08

                var authState = await authenticationStateTask;
                if (authState.User.Identity.IsAuthenticated)
                {
                    var identity = authState.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {

                        if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Do you want to apply?")) //for '{obj_JobDTO.Code}' job
                            return;

                        SCandidateEmail = await localStore.GetItemAsync<string>("Email");
                        if (SCandidateEmail != null && SCandidateEmail != "")
                        {
                            SCandidateEmail = SCandidateEmail.Substring(2);
                            SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);

                            Obj_Response = await _objRepoCan.GetCandidateAsync(SCandidateEmail);
                            if (Obj_Response.IsSuccess)
                            {
                                CandidateAddEditDTO obj_CandidateModel = JsonConvert.DeserializeObject<CandidateAddEditDTO>(Obj_Response.Data.ToString());
                                if (string.IsNullOrEmpty(obj_CandidateModel.ResumeURL))
                                {
                                    obj_snackbar.Add("Resume is required before applied job", Severity.Warning);
                                    return;
                                }
                            }
                        }

                        IsLoding = true;

                        obj_Application.CandidateNumber = obj_JobDTO.CandidateNumber;
                        obj_Application.JobERPRecId = obj_JobDTO.JobERPRecId;

                        obj_Application.ApplicationStatus = 0;
                        obj_Application.DataArea = "";
                        obj_Application.ERPRecId = 0;
                        obj_Application.AppliedDate = DateTime.Now;

                        Obj_Response = await _objRepo.ApplyJobApplication(obj_Application);
                        GetMassage();
                        IsLoding = false;
                        _nav.NavigateTo("/appliedjob");
                    }
                    else
                    {
                        localStorage.SetItemAsync("isUserApplyJob", true);
                        localStorage.SetItemAsync("JobERPRecId", obj_JobDTO.JobERPRecId);
                        _nav.NavigateTo("authentication/login");

                        //_nav.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_nav.Uri)}");
                    }
                }
                else
                {
                    localStorage.SetItemAsync("isUserApplyJob", true);
                    localStorage.SetItemAsync("JobERPRecId", obj_JobDTO.JobERPRecId);
                    _nav.NavigateTo("authentication/login");

                    //_nav.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_nav.Uri)}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                IsLoding = false;

            }
        }

        public void GetMassage()
        {
            if (Obj_Response.Status == "Success")
            {
                JobApply = "Applied";
                obj_snackbar.Add(Obj_Response.Message, Severity.Success);
            }
            else if (Obj_Response.Status == "Warning")
            {
                JobApply = "Applied";
                obj_snackbar.Add(Obj_Response.Message, Severity.Warning);
            }
            else
            {
                JobApply = "Apply";
                obj_snackbar.Add(Obj_Response.Message, Severity.Error);
            }
            StateHasChanged();
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

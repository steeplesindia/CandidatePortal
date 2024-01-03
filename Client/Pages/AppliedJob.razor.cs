using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Client.Model;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CandidatePortal.Client.Pages
{
    public partial class AppliedJob
    {
        private int _selected = 1;
        string _authMessage = "";

        [Inject]
        public IJobRepo _repo { get; set; }

        public JobDTO JobDTO { get; set; }
        public CandidateDetails obj_Candidate { get; set; }
        public ResponseStatusData Obj_Response = new ResponseStatusData();
        public List<JobAppliedDTO> JobDetailsList = new List<JobAppliedDTO>();

        public AppliedJobSearchModel JobsearchDTO = new AppliedJobSearchModel();

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        [Inject]
        public ISnackbar obj_snackbar { get; set; }

        [Inject]
        public IJobRepo obj_JobRepo { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        [Inject]
        public NavigationManager _nav { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        [Inject]
        Blazored.LocalStorage.ILocalStorageService localStore { get; set; }


        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Applied Job", href: null, icon: Icons.Material.Filled.AdsClick, disabled: true)
        };
        string SCandidateEmail = "";
        long CandidateNumber = 0;
        long JobView = 0;
        bool IsLoding = false;


        #region On Initilize

        protected override async Task OnInitializedAsync()
        {
            //_jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/downloadScript.js");

            IsLoding = true;
            SCandidateEmail = await localStore.GetItemAsync<string>("Email");
            if (SCandidateEmail != null && SCandidateEmail != "")
            {
                SCandidateEmail = SCandidateEmail.Substring(2);
                SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);

                await GetCandidateDetailsByEmailIdNew();
                await GetAppliedJobDetails();
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

            //        await GetCandidateDetailsByEmailIdNew();
            //        await GetAppliedJobDetails();
            //        IsLoding = false;

            //    }
            //}
            //else
            //{
            //    IsLoding = false;
            //    obj_snackbar.Add("Please Login", Severity.Error);
            //    _nav.NavigateTo("login");
            //}

            IsLoding = false;
        }
        #endregion

        public async Task ChangeAppliedJobStatusDetails(Int16 value)
        {
            JobsearchDTO.snStatus = value;
            await GetAppliedJobDetails();
        }

        public async Task ChangeAppliedJobSortByDetails(Int16 value)
        {
            JobsearchDTO.snSortBy = value;
            //await GetAppliedJobDetails();
            if (value == 0)
            {

                var JobDetailsList1 = JobDetailsList.OrderBy(x => x.Code).ToList();
                MappingData(JobDetailsList1);
            }
            else if (value == 1)
            {
                var JobDetailsList1 = JobDetailsList.OrderBy(x => x.AppliedDate).ToList();
                MappingData(JobDetailsList1);
            }
            else
            {
                var JobDetailsList1 = JobDetailsList.OrderBy(x => x.ApplicationStatus).ToList();
                MappingData(JobDetailsList1);
            }
            StateHasChanged();
        }

        public async Task MappingData(List<JobAppliedDTO> Data)
        {
            JobDetailsList = new List<JobAppliedDTO>();
            JobDetailsList = Data;
        }

        public async Task GetCandidateDetailsByEmailIdNew()
        {
            //Obj_Response = new ResponseStatusData();

            CandidateParameters dto = new();
            dto.PageNumber = 1;
            dto.PageSize = 1;

            //if (SCandidateEmail != "")
            //{
            //    SCandidateEmail = SCandidateEmail.Substring(2);
            //    SCandidateEmail = SCandidateEmail.Remove(SCandidateEmail.Length - 2);
            //}
            obj_Candidate = await obj_JobRepo.GetCandidateDetailsByEmailIdNew(SCandidateEmail);
            //if (Obj_Response.IsSuccess)
            //{
            //    obj_Candidate = JsonConvert.DeserializeObject<CandidateDetails>(Obj_Response.Data.ToString());
            //}
        }
        public async Task GetAppliedJobDetails()
        {
            if (obj_Candidate != null)
            {
                Obj_Response = await obj_JobRepo.GetAppliedJobByStatus(JobsearchDTO.snStatus, obj_Candidate.Number);
                if (Obj_Response.IsSuccess)
                {
                    JobDetailsList = JsonConvert.DeserializeObject<List<JobAppliedDTO>>(Obj_Response.Data.ToString());
                }
            }
        }

        public bool AlarmOn { get; set; }

        public void OnToggledChanged(bool toggled)
        {
            AlarmOn = toggled;

            if (AlarmOn)
            {
                JobView = 1;
                AlarmOn = true;
            }
            else
            {
                JobView = 0;
                AlarmOn = false;
            }
            StateHasChanged();
        }

        public void AddToApplyJob()
        {
            IsLoding = true;
            long JobERPRecId = 0;
            JobERPRecId = JobDTO.JobERPRecId;
            _nav.NavigateTo("JobDetail/" + JobERPRecId);
            IsLoding = false;
        }

        public async void signOut()
        {
            IsLoding = false;
            await SignOutManager.SetSignOutState();
            await localStore.ClearAsync();
            _nav.NavigateTo("/authentication/login");
        }

        //public async Task ClickFullView()
        //{
        //    StateHasChanged();
        //}
        //public async Task ClickHalfView()
        //{
        //    JobView = 0;
        //    StateHasChanged();
        //}
    }
}

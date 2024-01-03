using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Client.Componets;
using CandidatePortal.Client.Repository;
using CandidatePortal.Shared.DTO;
using CandidatePortal.Shared.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using MudBlazor;

namespace CandidatePortal.Client.Pages
{
    public partial class JobListing
    {
        private int _selected = 1;
        private bool bShowAdvSearch = false;
        string _SearchText = "";
        string _SearchLocationText = "";

        //string _LocationText = "";
        public ResponseStatusData Obj_Response = new();
        private List<JobDTO> lstJob = new();
        public MetaData MetaData { get; set; } = new MetaData();
        JobSearchParameter dto = new();

        //[Inject]
        //public SpinnerService obj_spinner { get; set; }

        [Inject]
        public NavigationManager navManager { get; set; }

        [Inject]
        public IJobRepo _objRepo { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Job Listing", href: null, icon: Icons.Material.Filled.AdsClick, disabled: true)
        };

        public bool AlarmOn { get; set; }
        long JobView = 0;

        bool IsLoding = false;

       
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

        private void ShowHideAdvSearch()
        {
            bShowAdvSearch = !bShowAdvSearch;
        }

        private async Task SelectedPage(int page)
        {
            _selected = page;
            await SearchJob();
        }

        protected override async Task OnInitializedAsync()
        {
            StringValues initSearch;
            var uri = navManager.ToAbsoluteUri(navManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("searchtext", out initSearch))
            {
                _SearchText = initSearch;
            }

            if (_SearchText != null && _SearchText != "" && _SearchText != "null")
            {
               await SearchJob();
            }
            else
            {       
                await GetDefaultJobList();
                _SearchText = "";
            }
        }

        public async Task SearchJob()
        {
            try
            {
                IsLoding = true;
                if ( _SearchText != "" || _SearchLocationText != "")
                {
                    dto.PageNumber = _selected;
                    dto.PageSize = 10;

                    if(_SearchText != null && _SearchText != "")
                    { 
                        dto.SearchTerm = _SearchText;
                    }
                    else
                    {
                        dto.SearchTerm = "null";
                    }

                    if (_SearchLocationText != null && _SearchLocationText != "")
                    {
                        dto.sLocationSearch = _SearchLocationText;
                    }
                    else
                    { 
                        dto.sLocationSearch = "null";
                    }

                    var pagingResponse = await _objRepo.SearchJob(dto);
                    lstJob = pagingResponse.Items;
                    MetaData = pagingResponse.MetaData;
                }
                else
                { 
                    await GetDefaultJobList();
                }
                IsLoding = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetDefaultJobList()
        {
            try
            {
                 _SearchText = "";
                 _SearchLocationText = "";

                IsLoding = true;
                dto.PageNumber = _selected;
                dto.PageSize = 10;
                dto.SearchTerm = _SearchText;

                var pagingResponse = await _objRepo.GetDefaultJobList(dto);
                lstJob = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
                IsLoding = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ApplicationStatus enumStatus { get; set; } = ApplicationStatus.All;
        public enum ApplicationStatus { All, Open, Closed, Applied }
    }
}

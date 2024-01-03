using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidatePortal.Client.Componets
{
    public partial class JobListingCard
    {
        [Parameter]
        public JobDTO JobDTO { get; set; }

        [Parameter]
        public long ApplyButton { get; set; }

        [Inject]
        public NavigationManager obj_navigation { get; set; }
       
        [Inject]
        public SpinnerService obj_spinner { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        //[CascadingParameter]
        //public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Parameter]
        public long JobView { get; set; }

        public void AddToApplyJob()
        {
            long JobERPRecId = 0;
            JobERPRecId = JobDTO.JobERPRecId;
            obj_navigation.NavigateTo("JobDetail/" + JobERPRecId);//+ "/" + ApplyButton
                
        }
    }
}

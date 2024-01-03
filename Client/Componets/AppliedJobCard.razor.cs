using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace CandidatePortal.Client.Componets
{
    public partial class AppliedJobCard
    {

        #region Declaretion

        [Parameter]
        public JobAppliedDTO JobAppliedDTO { get; set; }

        [Parameter]
        public long ApplyButton { get; set; }

        [Parameter]
        public long JobView { get; set; }
        
        [Inject]
        public NavigationManager obj_navigation { get; set; }

        [Inject]
        public SpinnerService obj_spinner { get; set; }

        [CascadingParameter]
        public ILanguageContainerService langaugecontainer { get; set; }

        DateTime? AppliedDate = null;
        string ApplicationStatus = "";

        protected override async Task OnInitializedAsync()
        {
            
        }

        public void AddToApplyJob()
        {
            obj_spinner.Show();
            long JobERPRecId = 0;
            JobERPRecId = JobAppliedDTO.JobERPRecId;
            obj_navigation.NavigateTo("JobDetail/" + JobERPRecId); // + "/" + ApplyButton
            obj_spinner.Hide();
        }
        public void ChatForHob()
        {
            obj_spinner.Show();
            long ApplicationNumber = 0;
            ApplicationNumber = JobAppliedDTO.ApplicationNumber;
            obj_navigation.NavigateTo("AppsCommunication/" + ApplicationNumber);
            obj_spinner.Hide();
        }
        public void ChecklistForHob()
        {
            obj_spinner.Show();
            long ApplicationNumber = 0;
            ApplicationNumber = JobAppliedDTO.ApplicationNumber;
            obj_navigation.NavigateTo("ApplicationChecklist/" + ApplicationNumber);
            obj_spinner.Hide();
        }

       public void QuestionaryForJob()
       {
            obj_spinner.Show();
            long ApplicationNumber = 0;
            ApplicationNumber = JobAppliedDTO.ApplicationNumber;
            obj_navigation.NavigateTo("questionaries/" + ApplicationNumber);
            obj_spinner.Hide();
        }

    #endregion
}
}

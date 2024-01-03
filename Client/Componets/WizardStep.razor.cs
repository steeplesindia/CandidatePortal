using CandidatePortal.Client.Pages;
using Microsoft.AspNetCore.Components;

namespace CandidatePortal.Client.Componets
{
    public partial class WizardStep
    {
        /// <summary>
        /// The <see cref="Wizard"/> container
        /// </summary>
        [CascadingParameter]
        protected internal Wizard Parent { get; set; }

        /// <summary>
        /// The Child Content of the current <see cref="WizardStep"/>
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The Name of the step
        /// </summary>
        [Parameter]
        public string Name { get; set; }


        protected override void OnInitialized()
        {
            Parent.AddStep(this);
        }
    }
}

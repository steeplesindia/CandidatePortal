using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CandidatePortal.Client
{
    public class SpinnerService
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }

        public async void RedirectToLogin()
        {
            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("/authentication/login");
        }
    }
}

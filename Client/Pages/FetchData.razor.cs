
using CandidatePortal.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Security.Claims;


namespace CandidatePortal.Client.Pages
{
    public partial class FetchData
    {

        private WeatherForecast[]? forecasts;
        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
        private string _authMessage;
        private string _surnameMessage;

        //[Inject]
        //public AuthenticationStateProvider AuthenticationState { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthenticationState.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity != null && user.Identity.IsAuthenticated)
                {
                    _authMessage = $"{user.Identity.Name} is authenticated.";
                    _claims = user.Claims;
                    _surnameMessage =
                        $"Surname: {user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value}";
                }
                else
                {
                    _authMessage = "The user is NOT authenticated.";
                }

                //var accessToken = await HttpContext.GetTokenAsync("access_token");
                //var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

                forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
    }
}

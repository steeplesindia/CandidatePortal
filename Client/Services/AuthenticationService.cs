using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProtectedLocalStore;
using System.Security.Claims;

namespace CandidatePortal.Client.Services
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        private const string USER_SESSION_OBJECT_KEY = "user_session_obj";
        private const string ACCESS_TOKEN = "accesstoken";
        private const string USER_PERMISSIONS = "userpermissions";

        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;


        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        public AuthenticationService(ProtectedLocalStorage protectedSessionStore, IHttpContextAccessor httpContextAccessor)
        {
            _protectedLocalStorage = protectedSessionStore;
            _httpContextAccessor = httpContextAccessor;
        }

        //public string IpAddress => _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;

        //private User User { get; set; }
        //private List<UserPermission> UserPermissionList { get; set; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //        User userSession = await GetUserSession();
                //        List<UserPermission> userPermissions = await GetUserPermission();

                //        if (userSession != null)
                //            return await GenerateAuthenticationState(userSession, userPermissions);
                       return await GenerateEmptyAuthenticationState();
            }
            catch
            {
                await LogoutAsync();
                return null;
            }

        }

        //public async Task LoginAsync(User user, List<UserPermission> userPermissions)
        //{
        //    await SetUserSession(user);
        //    await SetUserPermissionSession(userPermissions);

        //    NotifyAuthenticationStateChanged(GenerateAuthenticationState(user, userPermissions));
        //}

        public async Task LogoutAsync()
        {
            //    //await SetUserSession(null);
            //    RefreshUserSession(null);
            //    await _protectedLocalStorage.DeleteAsync(USER_SESSION_OBJECT_KEY);
            //    await _protectedLocalStorage.DeleteAsync(ACCESS_TOKEN);
            //    await _protectedLocalStorage.DeleteAsync(USER_PERMISSIONS);

            //    NotifyAuthenticationStateChanged(GenerateEmptyAuthenticationState());

            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("/authentication/login");
        }

            //public async Task<User> GetUserSession()
            //{
            //    if (User != null)
            //        return User;
            //    //TODO burda localUserJson get yaparken hata alıyor. try catch işi çözmezse buraya tekrardan bakılacak.
            //    try
            //    {
            //        var localUserJson = await _protectedLocalStorage.GetAsync<string>(USER_SESSION_OBJECT_KEY);

            //        if (string.IsNullOrEmpty(localUserJson.Value))
            //            return null;

            //        return RefreshUserSession(JsonConvert.DeserializeObject<User>(localUserJson.Value));
            //    }
            //    catch
            //    {
            //        await LogoutAsync();
            //        return null;
            //    }
            //}
            //public async Task<List<UserPermission>> GetUserPermission()
            //{
            //    if (UserPermissionList != null)
            //        return UserPermissionList;
            //    try
            //    {
            //        var localUserPermissionJson = await _protectedLocalStorage.GetAsync<string>(USER_PERMISSIONS);

            //        if (string.IsNullOrEmpty(localUserPermissionJson.Value))
            //            return null;

            //        return RefreshUserPermissionSession(JsonConvert.DeserializeObject<List<UserPermission>>(localUserPermissionJson.Value));
            //    }
            //    catch
            //    {
            //        await LogoutAsync();
            //        return null;
            //    }
            //}
            //private async Task SetUserSession(User user)
            //{
            //    RefreshUserSession(user);

            //    await _protectedLocalStorage.SetAsync(USER_SESSION_OBJECT_KEY, JsonConvert.SerializeObject(user));
            //}

            //private async Task SetUserPermissionSession(List<UserPermission> userPermissions)
            //{
            //    RefreshUserPermissionSession(userPermissions);

            //    await _protectedLocalStorage.SetAsync(USER_PERMISSIONS, JsonConvert.SerializeObject(userPermissions));
            //}

            //private User RefreshUserSession(User user) => User = user;
            //private List<UserPermission> RefreshUserPermissionSession(List<UserPermission> userPermission) => UserPermissionList = userPermission;

            //private Task<AuthenticationState> GenerateAuthenticationState(User user, List<UserPermission> userPermission)
            //{
            //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Id.ToString()),
            //        new Claim(ClaimTypes.Role, userPermission.ToString()),
            //    }, "auth");

            //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            //    return Task.FromResult(new AuthenticationState(claimsPrincipal));
            //}

            private Task<AuthenticationState> GenerateEmptyAuthenticationState() => Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
}

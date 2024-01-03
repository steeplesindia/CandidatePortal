using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CandidatePortal.Client;
using MudBlazor.Services;
using System.Reflection;
using System.Globalization;
using AKSoftware.Localization.MultiLanguages;
using CandidatePortal.Client.Services;
using CandidatePortal.Client.Repository;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;
using Blazored.LocalStorage;

var baseurlstring = "";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"));

//builder.Configuration.AddJsonFile("appsettings.json");
//IConfiguration _Configuration;

builder.Services.AddHttpClient("CandidatePortal.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/")).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient("CandidatePortal.ServerAPI.Public", 
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/"));

    builder.Services.AddMsalAuthentication(options =>
    {
        options.ProviderOptions.Cache.CacheLocation = "localStorage";
    });

builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddScoped<ProtectedLocalStorage>();
//builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<ICandidateRepo, CandidateManager>();
builder.Services.AddScoped<IJobRepo, JobManager>();
builder.Services.AddScoped<IApplicationCommunication, ApplicationCommunicationManager>();
builder.Services.AddScoped<IApplicationChecklist, ApplicationCheckListManager>();
builder.Services.AddScoped<IApplicationQuestionaires, ApplicationQuestionairesManager>();

builder.Services.AddScoped<SpinnerService>();
//builder.Services.AddScoped<SpinnerHandler>();
builder.Services.AddScoped(s =>
{
    //SpinnerHandler spinHandler = s.GetRequiredService<SpinnerHandler>();
    //spinHandler.InnerHandler = new HttpClientHandler();
    NavigationManager navManager = s.GetRequiredService<NavigationManager>();
    return new HttpClient()/*spinHandler*/
    {
        BaseAddress = new Uri(navManager.BaseUri)
    };
});



builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CandidatePortal.ServerAPI"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CandidatePortal.ServerAPI.Public"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://steeplesb2c.onmicrosoft.com/c414679a-4c9f-492c-80b4-55de01934e33/db_read_write");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://qfcrab2c.onmicrosoft.com/d6f70325-9f7b-4838-a55c-667b48cc1f62/db_read_write");
    options.ProviderOptions.LoginMode = "Redirect";
    var authentication = options.ProviderOptions.Authentication;
    authentication.PostLogoutRedirectUri = "/authentication/login";//builder.Configuration.GetSection("AzureAdB2C:CallbackPath").ToString();
    //authentication.ValidateAuthority = false;
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://steeplesb2c.onmicrosoft.com/c414679a-4c9f-492c-80b4-55de01934e33/db_read_write");
});
builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

//builder.services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
//            .AddAzureAD(options => Configuration.Bind("AzureAd", options))
//            .AddCookie();

//builder.services.Configure<OpenIdConnectOptions>(AzureADDefaults.AuthenticationScheme, options =>
//{
//    options.SaveTokens = true;
//});

await builder.Build().RunAsync();

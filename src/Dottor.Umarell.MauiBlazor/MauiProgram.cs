namespace Dottor.Umarell.MauiBlazor
{
    using Dottor.Umarell.Client.Services;
    using Dottor.Umarell.MauiBlazor.Services;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebView.Maui;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.JSInterop;
    using System.Security.Claims;

    public static class MauiProgram
    {
        public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2" : "http://localhost";
        public static string APIUrl = $"https://localhost:7103/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(APIUrl) });
            builder.Services.AddScoped<ApiProxyService>();

            builder.Services.TryAddSingleton<AuthenticationStateProvider, ExternalAuthStateProvider>();
            builder.Services.TryAddSingleton(sp => (ExternalAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
            builder.Services.AddSingleton<AuthenticatedUser>();

            //builder.Services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            //builder.Services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());

            builder.Services.AddDottorUI();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            var host = builder.Build();

            var authenticatedUser = host.Services.GetRequiredService<AuthenticatedUser>();
            /*
            Provide OpenID/MSAL code to authenticate the user. See your identity provider's 
            documentation for details.

            The user is represented by a new ClaimsPrincipal based on a new ClaimsIdentity.
            */
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Andrea"),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                }
                ));

            authenticatedUser.Principal = user;
            return host;
        }
    }
}
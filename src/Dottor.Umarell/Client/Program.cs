using Dottor.Umarell.Client;
using Dottor.Umarell.Client.Services;
using Dottor.Umarell.UI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("it-IT");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("it-IT");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddScoped<ApiProxyService>();
builder.Services.AddUmarellUI();

await builder.Build().RunAsync();

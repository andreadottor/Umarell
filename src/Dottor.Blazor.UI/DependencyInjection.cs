namespace Microsoft.Extensions.DependencyInjection;

using Dottor.Blazor.UI.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddDottorUI(this IServiceCollection service)
    {
        service.AddScoped<IMessageBoxService, MessageBoxService>();
        service.AddScoped<IDownloadFileService, DownloadFileService>();
        return service;
    }
}

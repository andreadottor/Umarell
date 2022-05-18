namespace Dottor.Blazor.UI
{
    using Dottor.Blazor.UI.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddDottorUI(this IServiceCollection service)
        {
            service.AddScoped<IMessageBoxService, MessageBoxService>();
            service.AddScoped<IDownloadFileService, DownloadFileService>();
            return service;
        }
    }
}

namespace Dottor.Umarell.UI
{
    using Dottor.Umarell.UI.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddUmarellUI(this IServiceCollection service)
        {
            service.AddSingleton<IMessageBoxService, MessageBoxService>();
            return service;
        }
    }
}

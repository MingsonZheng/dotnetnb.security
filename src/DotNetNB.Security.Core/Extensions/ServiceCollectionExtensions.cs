using DotNetNB.Security.Core.Store;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, Action<SecurityOption>? configure)
        {
            var option = new SecurityOption { Services = services };
            configure?.Invoke(option);

            services.AddSingleton<IResourceStore, DefaultResourceStore>()
                .AddSingleton<IPermissionStore, DefaultPermissionStore>()
                .AddScoped<IResourceManager, ResourceManager>()
                .AddScoped<IPermissionManager, PermissionManager>()
                .AddHostedService<ResourceProviderHostedService>();
            return services;
        }
    }
}

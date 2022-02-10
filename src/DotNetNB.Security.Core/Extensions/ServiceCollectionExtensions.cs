using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, Action<SecurityOption>? configure)
        {
            services.AddHostedService<ResourceProviderHostedService>();
            return services;
        }
    }
}

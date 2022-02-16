using DotNetNB.Security.Core.Store;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static ISecurityBuilder AddDotNetNBSecurity(this IServiceCollection services, Action<SecurityBuilder>? configure)
    {
        var builder = new SecurityBuilder() { Services = services };
        configure?.Invoke(builder);

        services
            .AddSingleton<IResourceStore, DefaultResourceStore>()
            .AddSingleton<IPermissionStore, DefaultPermissionStore>()
            .AddScoped<IClaimsProviderFactory, DefaultClaimsProviderFactory>()
            .AddScoped<IResourceManager, ResourceManager>()
            .AddScoped<IPermissionManager, PermissionManager>()
            .AddHostedService<ResourceProviderHostedService>();

        return builder;
    }
}
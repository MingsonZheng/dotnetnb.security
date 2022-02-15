using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNB.Security.ActionAccess;

public static class SecurityOptionExtensions
{
    public static SecurityBuilder AddActionAccessControl(this SecurityBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IResourceProvider, ActionResourceProvider>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IClaimsProvider, ActionClaimProvider>());
        
        builder.Services
            .AddSingleton<IAuthorizationMiddlewareResultHandler, DotNetNBAuthorizationMiddlewareResultHandler>();
        return builder;
    }
}
using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNB.Security.EntityAccess.Extensions;

public static class SecurityOptionExtensions
{
    public static IEntityAccessBuilder AddEntityAccessControl(this SecurityBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IResourceProvider, EntityResourceProvider>());
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IClaimsProvider, EntityClaimsProvider>());
        
        var entityAccessBuilder = new EntityAccessBuilder();
        builder.Services.AddSingleton<IEntityAccessBuilder>(sp => entityAccessBuilder);
        return entityAccessBuilder;
    }
}
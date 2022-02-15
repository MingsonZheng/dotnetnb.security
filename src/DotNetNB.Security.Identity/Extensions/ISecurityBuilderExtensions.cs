using DotNetNB.Security.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Identity.Extensions;

public static class ISecurityBuilderExtensions
{
    public static IDotNetNBIdentityBuilder AddIdentity(this ISecurityBuilder builder)
    {
        return builder.AddIdentity<IdentityUser, IdentityRole>();
    }

    public static IDotNetNBIdentityBuilder AddIdentity<TUser,TRole>(this ISecurityBuilder builder)
    where TUser: IdentityUser
    where TRole: IdentityRole
    {
        var identityBuilder = builder.Services.AddIdentity<TUser, TRole>()
            .AddDefaultTokenProviders();

        identityBuilder.Services
            .AddScoped<IRolePermissionManager<TRole>, RolePermissionManager<TRole>>()
            .AddScoped<IUserPermissionManager<TUser>, UserPermissionManager<TUser>>();
        
        return new DotNetNBIdentityBuilder() {IdentityBuilder = identityBuilder};
    }
    
    
}
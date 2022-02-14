using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Identity.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder WithPermissions<TUser, TRole>(this IdentityBuilder identityBuilder)
            where TRole : class where TUser : class
        {
            identityBuilder.Services.AddScoped<IRolePermissionManager<TRole>, RolePermissionManager<TRole>>()
                .AddScoped<IUserPermissionManager<TUser>, UserPermissionManager<TUser>>();
            return identityBuilder;
        }
    }
}

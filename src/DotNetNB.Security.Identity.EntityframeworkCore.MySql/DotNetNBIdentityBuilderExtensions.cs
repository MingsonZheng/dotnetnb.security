using DotNetNB.Security.Identity.EntityframeworkCore.MySql.Store;
using DotNetNB.Security.Identity.Extensions;
using DotNetNB.Security.Identity.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql;

public static class DotNetNBIdentityBuilderExtensions
{
    public static IDotNetNBIdentityBuilder AddEntityframeworkCore(
        this IDotNetNBIdentityBuilder builder,
        IConfiguration configuration,
        string connectionString,
        MySqlServerVersion version)
    {
        builder.IdentityBuilder.AddEntityFrameworkStores<DotNetNBIdentityDbContext>();
        
        builder.IdentityBuilder.Services.AddDbContext<DotNetNBIdentityDbContext>(options=>options.UseMySql(connectionString, version));
        builder.IdentityBuilder.Services.Configure<IdentityData>(configuration.GetSection("IdentityData"));
        builder.IdentityBuilder.Services.AddHostedService<DbMigrationService>();

        builder.IdentityBuilder.Services.AddScoped<IUserPermissionStore, UserPermissionStore>();
        builder.IdentityBuilder.Services.AddScoped<IRolePermissionStore, RolePermissionStore>();
        
        return builder;
    }
    
    
}
using DotNetNB.Security.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql;

public class DotNetNBIdentityDbContext :  IdentityDbContext<IdentityUser>
{
    public DotNetNBIdentityDbContext(DbContextOptions<DotNetNBIdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RolePermission>().ToTable("AspNetUserRolePermission")
            .HasKey(e=>new{ e.PermissionKey, e.RoleName});

        builder.Entity<UserPermission>().ToTable("AspNetUserPermission")
            .HasKey(e => new {e.PermissionKey, e.UserName});
        
        base.OnModelCreating(builder);
    }

    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

}

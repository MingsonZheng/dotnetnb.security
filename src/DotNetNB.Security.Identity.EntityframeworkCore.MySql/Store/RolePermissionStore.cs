using DotNetNB.Security.Identity.Models;
using DotNetNB.Security.Identity.Store;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql.Store;

public class RolePermissionStore: IRolePermissionStore
{
    private readonly DotNetNBIdentityDbContext _dbContext;
    public RolePermissionStore(DotNetNBIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateRolePermission(RolePermission permission)
    {
        _dbContext.RolePermissions.Add(permission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<RolePermission>> FindRolePermissions(string name)
    {
        var rolePermissions = await _dbContext.RolePermissions.Where(r => r.RoleName == name).ToListAsync();
        return rolePermissions;
    }

    public async Task<RolePermission> FindRolePermission(string roleName, string permissionKey)
    {
        return await _dbContext.RolePermissions.FirstOrDefaultAsync(r =>
            r.RoleName == roleName && r.PermissionKey == permissionKey);
    }
}
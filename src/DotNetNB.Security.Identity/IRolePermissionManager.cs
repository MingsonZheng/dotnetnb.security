using DotNetNB.Security.Identity.Models;

namespace DotNetNB.Security.Identity;

public interface IRolePermissionManager<TRole>
{
    public Task AddRolePermission(string name, string permissionKey);

    public Task<IEnumerable<RolePermission>> FindRolePermission(string name);
}
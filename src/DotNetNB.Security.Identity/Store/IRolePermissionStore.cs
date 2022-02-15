using DotNetNB.Security.Identity.Models;

namespace DotNetNB.Security.Identity.Store;

public interface IRolePermissionStore
{
    Task CreateRolePermission(RolePermission permission);

    Task<IEnumerable<RolePermission>> FindRolePermissions(string name);

    Task<RolePermission> FindRolePermission(string roleName, string permissionKey);
}
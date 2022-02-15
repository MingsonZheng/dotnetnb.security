using DotNetNB.Security.Identity.Models;

namespace DotNetNB.Security.Identity.Store;

public interface IUserPermissionStore
{
    Task CreateUserPermission(UserPermission permission);

    Task<IEnumerable<UserPermission>> FindUserPermissions(string username);

    Task<UserPermission> FindUserPermission(string username, string permissionKey);
}
using DotNetNB.Security.Identity.Models;

namespace DotNetNB.Security.Identity;

public interface IUserPermissionManager<TUser>
{
    public Task AddUserPermission(string username, string permissionKey);

    public Task<IEnumerable<UserPermission>> FindUserPermission(string username);
}
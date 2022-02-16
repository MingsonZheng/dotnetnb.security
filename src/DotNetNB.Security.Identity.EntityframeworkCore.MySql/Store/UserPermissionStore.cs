using DotNetNB.Security.Identity.Models;
using DotNetNB.Security.Identity.Store;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql.Store;

public class UserPermissionStore: IUserPermissionStore
{
    private readonly DotNetNBIdentityDbContext _dbContext;
    public UserPermissionStore(DotNetNBIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateUserPermission(UserPermission permission)
    {
        _dbContext.UserPermissions.Add(permission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserPermission>> FindUserPermissions(string username)
    {
        var userPermission = await _dbContext.UserPermissions.Where(u => u.UserName == username).ToListAsync();
        return userPermission;
    }

    public async Task<UserPermission> FindUserPermission(string username, string permissionKey)
    {
        return await _dbContext.UserPermissions.SingleOrDefaultAsync(u =>
            u.UserName == username && u.PermissionKey == permissionKey);
    }
}
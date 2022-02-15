using DotNetNB.Security.Core;
using DotNetNB.Security.Identity.Models;
using DotNetNB.Security.Identity.Store;
using Microsoft.AspNetCore.Identity;

namespace DotNetNB.Security.Identity;

public class UserPermissionManager<TUser> : IUserPermissionManager<TUser> where TUser : class
{
    private readonly UserManager<TUser> _userManager;
    private readonly IPermissionManager _permissionManager;
    private readonly IUserPermissionStore _userPermissionStore;
    private readonly IClaimsProviderFactory _claimsProviderFactory;

    public UserPermissionManager(UserManager<TUser> userManager,
        IPermissionManager permissionManager,
        IUserPermissionStore userPermissionStore,
        IClaimsProviderFactory claimsProviderFactory
        )
    {
        _userManager = userManager;
        _permissionManager = permissionManager;
        _userPermissionStore = userPermissionStore;
        _claimsProviderFactory = claimsProviderFactory;
    }

    public async Task AddUserPermission(string username, string permissionKey)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            throw new InvalidOperationException($"User not found:{username}");

        var permission = await _permissionManager.GetAsync(permissionKey);
        if (permission == null)
            throw new InvalidOperationException($"Permission not found:{permissionKey}");

        var origin = await _userPermissionStore.FindUserPermission(username, permissionKey);
        if (origin != null)
            throw new InvalidOperationException(
                $"User Permission Already Exist user:{username}, permission:{permissionKey}");

        var claimsProvider = _claimsProviderFactory.CreateProvider(permission);
        var claims = await claimsProvider.GetClaims(permission);

        await _userManager.AddClaimsAsync(user, claims);
        await _userPermissionStore.CreateUserPermission(new UserPermission()
        { PermissionKey = permissionKey, UserName = username });
    }

    public async Task<IEnumerable<UserPermission>> FindUserPermission(string username)
    {
        return await _userPermissionStore.FindUserPermissions(username);
    }
}
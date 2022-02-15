using DotNetNB.Security.Core;
using DotNetNB.Security.Identity.Models;
using DotNetNB.Security.Identity.Store;
using Microsoft.AspNetCore.Identity;

namespace DotNetNB.Security.Identity;

public class RolePermissionManager<TRole> : IRolePermissionManager<TRole> where TRole : class
{
    private readonly IPermissionManager _permissionManager;
    private readonly RoleManager<TRole> _roleManager;
    private readonly IRolePermissionStore _rolePermissionStore;
    private readonly IClaimsProviderFactory _claimsProviderFactory;

    public RolePermissionManager(IPermissionManager permissionManager,
        RoleManager<TRole> roleManager,
        IRolePermissionStore rolePermissionStore,
            IClaimsProviderFactory claimsProviderFactory
        )
    {
        _permissionManager = permissionManager;
        _roleManager = roleManager;
        _rolePermissionStore = rolePermissionStore;
        _claimsProviderFactory = claimsProviderFactory;
    }

    public async Task AddRolePermission(string name, string permissionKey)
    {
        var role = await _roleManager.FindByNameAsync(name);
        if (role == null)
            throw new InvalidOperationException($"Role not found:{name}");

        var permission = await _permissionManager.GetAsync(permissionKey);
        if (permission == null)
            throw new InvalidOperationException($"Permission not found:{permissionKey}");

        var origin = await _rolePermissionStore.FindRolePermission(name, permissionKey);
        if (origin != null)
            throw new InvalidOperationException(
                $"Role Permission Already Exist role:{name}, permission:{permissionKey}");

        var claimsProvider = _claimsProviderFactory.CreateProvider(permission);
        var claims = await claimsProvider.GetClaims(permission);

        foreach (var claim in claims)
        {
            await _roleManager.AddClaimAsync(role, claim);
        }

        await _rolePermissionStore.CreateRolePermission(new RolePermission()
        { PermissionKey = permissionKey, RoleName = name });
    }

    public async Task<IEnumerable<RolePermission>> FindRolePermission(string name)
    {
        return await _rolePermissionStore.FindRolePermissions(name);
    }
}
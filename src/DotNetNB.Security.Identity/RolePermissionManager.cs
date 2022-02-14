using DotNetNB.Security.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DotNetNB.Security.Identity
{
    public class RolePermissionManager<TRole> : IRolePermissionManager<TRole> where TRole : class
    {
        private readonly IPermissionManager _permissionManager;
        private readonly RoleManager<TRole> _roleManager;

        public RolePermissionManager(IPermissionManager permissionManager, RoleManager<TRole> roleManager)
        {
            _permissionManager = permissionManager;
            _roleManager = roleManager;
        }

        public async Task AddRolePermission(string roleId, string permissionKey) 
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new InvalidOperationException($"Role not found:{roleId}");
            }

            var permission = await _permissionManager.GetAsync(permissionKey);
            if (permission == null)
            {
                throw new InvalidOperationException($"Permission not found:{permissionKey}");
            }
            foreach (var resource in permission.Resources)
            {
                await _roleManager.AddClaimAsync(role, new Claim(ClaimsTypes.Permission, resource.Key));
            }

            //TBD 持久化 permission 和 role 的关系
        }
    }
}

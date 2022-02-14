using System.Security.Claims;
using DotNetNB.Security.Core;
using Microsoft.AspNetCore.Identity;

namespace DotNetNB.Security.Identity
{
    public class UserPermissionManager<TUser> : IUserPermissionManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IPermissionManager _permissionManager;

        public UserPermissionManager(UserManager<TUser> userManager, IPermissionManager permissionManager)
        {
            _userManager = userManager;
            _permissionManager = permissionManager;
        }

        public async Task AddUserPermission(string userId, string permissionKey)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User not found:{userId}");
            }

            var permission = await _permissionManager.GetAsync(permissionKey);
            if (permission == null)
            {
                throw new InvalidOperationException($"Permission not found:{permissionKey}");
            }

            var claims = permission.Resources.Select(p => new Claim(ClaimsTypes.Permission, p.Key)).ToList();
            await _userManager.AddClaimsAsync(user, claims);
        }
    }
}

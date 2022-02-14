namespace DotNetNB.Security.Identity
{
    public interface IRolePermissionManager<TRole>
    {
        public Task AddRolePermission(string roleId, string permissionKey);
    }
}

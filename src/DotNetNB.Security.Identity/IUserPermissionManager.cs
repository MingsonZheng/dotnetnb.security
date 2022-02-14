namespace DotNetNB.Security.Identity
{
    public interface IUserPermissionManager<TUser>
    {
        public Task AddUserPermission(string userId, string permissionKey);
    }
}

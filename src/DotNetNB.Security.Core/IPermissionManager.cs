using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core
{
    public interface IPermissionManager
    {
        public Task CreateAsync(string key, string displayName, string description, IEnumerable<string> resources);

        public Task<Permission> GetAsync(string key);

        public Task<IEnumerable<Permission>> GetAllAsync();
    }
}

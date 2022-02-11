using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core
{
    public interface IPermissionManager
    {
        public Task Create(string key, string displayName, string description, IEnumerable<string> resources);

        public Task<IEnumerable<Permission>> GetAllAsync();
    }
}

using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core.Store
{
    public interface IResourceStore
    {
        public Task CreateAsync(Resource resource);

        public Task CreateAsync(IEnumerable<Resource> resources);

        public Task<IEnumerable<Resource>> GetAllAsync();

        public Task<Resource> GetByKeyAsync(string key);

        public Task<IEnumerable<Resource>> GetByKeysAsync(IEnumerable<string> resources);
    }
}

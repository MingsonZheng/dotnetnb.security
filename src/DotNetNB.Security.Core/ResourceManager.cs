using DotNetNB.Security.Core.Models;
using DotNetNB.Security.Core.Store;

namespace DotNetNB.Security.Core
{
    public class ResourceManager :  IResourceManager
    {
        private readonly IResourceStore _resourceStore;
        public ResourceManager(IResourceStore resourceStore)
        {
            _resourceStore = resourceStore;
        }

        public async Task CreateAsync(Resource resource)
        {
            var origin = await _resourceStore.GetByKeyAsync(resource.Key);
            if (origin != null)
                throw new InvalidOperationException("Duplicated resource key found");

            await _resourceStore.CreateAsync(resource);
        }

        public async Task CreateAsync(IEnumerable<Resource> resources)
        {
            var origins = await _resourceStore.GetByKeysAsync(resources.Select(r => r.Key));
            if (origins.Any())
                throw new InvalidOperationException($"Duplicated resource key found:{string.Concat(origins.Select(o => o.Key), ",")}");

            await _resourceStore.CreateAsync(resources);
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await _resourceStore.GetAllAsync();
        }

        public async Task<IEnumerable<Resource>> GetByKeysAsync(IEnumerable<string> resources)
        {
            return await _resourceStore.GetByKeysAsync(resources);
        }
    }
}
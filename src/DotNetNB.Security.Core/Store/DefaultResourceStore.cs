using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core.Store
{
    public class DefaultResourceStore : IResourceStore
    {
        private readonly List<Resource> _list;

        public DefaultResourceStore()
        {
            _list = new List<Resource>();
        }

        public async Task CreateAsync(Resource resource)
        {
            _list.Add(resource);
        }

        public async Task CreateAsync(IEnumerable<Resource> resources)
        {
            _list.AddRange(resources);
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return _list;
        }

        public async Task<Resource> GetByKeyAsync(string key)
        {
            return _list.SingleOrDefault(r => r.Key == key);
        }

        public async Task<IEnumerable<Resource>> GetByKeysAsync(IEnumerable<string> resources)
        {
            return _list.Where(r => resources.Contains(r.Key));
        }
    }
}

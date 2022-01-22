using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core
{
    public interface IResourceManager
    {
        public Task CreateAsync(Resource resource);

        public Task CreateAsync(IEnumerable<Resource> resources);
    }
}

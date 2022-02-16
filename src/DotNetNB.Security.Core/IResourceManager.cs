using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public interface IResourceManager
{
    public Task CreateAsync(Resource resource);

    public Task CreateAsync(IEnumerable<Resource> resources);

    public Task<IEnumerable<Resource>> GetAllAsync();

    public Task<IEnumerable<Resource>> GetByKeysAsync(IEnumerable<string> resources);

    public Task<IEnumerable<Resource>> GetByGroupAsync(string group);
}
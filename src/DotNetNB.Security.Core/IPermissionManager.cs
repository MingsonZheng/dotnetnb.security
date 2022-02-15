using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public interface IPermissionManager
{
    public Task CreateAsync(string key, string group, string displayName, string description, IEnumerable<string> resources);

    public Task CreateAsync(Permission permission);

    public Task<IEnumerable<Permission>> GetAllAsync();

    public Task<Permission> GetAsync(string key);

    public Task<IEnumerable<Permission>> GetByGroupAsync(string @group);
}
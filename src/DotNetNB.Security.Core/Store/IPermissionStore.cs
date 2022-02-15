using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core.Store;

public interface IPermissionStore
{
    public Task CreateAsync(Permission permission);

    public Task<Permission> GetByKeyAsync(string key);

    public Task<IEnumerable<Permission>> GetAllAsync();

    public Task<IEnumerable<Permission>> GetByGroupAsync(string @group);
}
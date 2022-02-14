using DotNetNB.Security.Core.Models;
using DotNetNB.Security.Core.Store;

namespace DotNetNB.Security.Core;

public class PermissionManager : IPermissionManager
{
    private readonly IResourceManager _resourceManager;
    private readonly IPermissionStore _permissionStore;

    public PermissionManager(IResourceManager resourceManager, IPermissionStore permissionStore)
    {
        _resourceManager = resourceManager;
        _permissionStore = permissionStore;
    }

    public async Task CreateAsync(string key, string displayName, string description, IEnumerable<string> resourceKeys)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        var origin = await _permissionStore.GetByKeyAsync(key);
        if (origin != null)
            throw new InvalidOperationException("Duplicated permission key found");

        var permission = new Permission { Key = key, DisplayName = displayName, Description = description };
        var resources = await _resourceManager.GetByKeysAsync(resourceKeys);
        permission.Resources = resources;

        await _permissionStore.CreateAsync(permission);
    }

    public async Task<Permission> GetAsync(string key)
    {
        return await _permissionStore.GetByKeyAsync(key);
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _permissionStore.GetAllAsync();
    }
}
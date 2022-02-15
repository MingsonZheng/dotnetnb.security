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

    public async Task CreateAsync(string key, string group, string displayName, string description, IEnumerable<string> resourceKeys)
    {
        var permission = new Permission()
        {
            Key = key,
            Group = group,
            DisplayName = displayName,
            Description = description,
            Resources = resourceKeys.Select(r => new Resource() { Key = r })
        };

        await CreateAsync(permission);
    }

    public async Task CreateAsync(Permission permission)
    {
        if (string.IsNullOrEmpty(permission.Key))
            throw new ArgumentNullException(nameof(permission.Key));

        if (string.IsNullOrEmpty(permission.Group))
            throw new ArgumentNullException(nameof(permission.Group));

        var origin = await _permissionStore.GetByKeyAsync(permission.Key);
        if (origin != null)
            throw new InvalidOperationException("Duplicated permission key found");

        var resourceKeys = permission.Resources?.Select(r => r.Key);
        var resources = await _resourceManager.GetByKeysAsync(resourceKeys);
        if (!resources.Any())
            throw new InvalidOperationException("invalid resource list");

        permission.Resources = resources;
        await _permissionStore.CreateAsync(permission);
    }

    public async Task<Permission> GetAsync(string key)
    {
        return await _permissionStore.GetByKeyAsync(key);


    }

    public async Task<IEnumerable<Permission>> GetByGroupAsync(string @group)
    {
        return await _permissionStore.GetByGroupAsync(group);
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _permissionStore.GetAllAsync();
    }
}
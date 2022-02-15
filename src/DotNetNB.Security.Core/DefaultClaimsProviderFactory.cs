using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public class DefaultClaimsProviderFactory: IClaimsProviderFactory
{
    private IEnumerable<IClaimsProvider> _providers;

    public DefaultClaimsProviderFactory(IEnumerable<IClaimsProvider> provider)
    {
        _providers = provider;
    }
    
    public IClaimsProvider CreateProvider(Permission permission)
    {
        var resourceType = permission.Resources.FirstOrDefault()?.Type;
        if (string.IsNullOrEmpty(resourceType))
            throw new InvalidOperationException("Unexpected resource type null or empty");

        var provider = _providers.FirstOrDefault(p => p.ResourceType == resourceType);
        if (provider == null)
            throw new InvalidOperationException($"Claim Provider not found for resource type {resourceType}");
        
        return provider;
    }
}
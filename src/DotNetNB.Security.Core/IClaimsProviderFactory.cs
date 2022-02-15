using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public interface IClaimsProviderFactory
{
    public IClaimsProvider CreateProvider(Permission permission);
}
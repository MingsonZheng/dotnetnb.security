using System.Security.Claims;
using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.EntityAccess;

public class EntityClaimsProvider: IClaimsProvider
{
    public string ResourceType { get; set; } = "Entity";
    
    public async Task<IEnumerable<Claim>> GetClaims(Permission permission)
    {
        return new List<Claim>() {new Claim(ClaimsTypes.Permission, permission.Key)};
    }
}
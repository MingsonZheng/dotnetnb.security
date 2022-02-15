using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;
using System.Security.Claims;

namespace DotNetNB.Security.ActionAccess;

public class ActionClaimProvider: IClaimsProvider
{
    public string ResourceType { get; set; } = "Action";

    public async Task<IEnumerable<Claim>> GetClaims(Permission permission)
    {
        return permission.Resources.Select(r => new Claim(ClaimsTypes.Permission, r.Key));
    }
}
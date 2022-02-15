using System.Security.Claims;
using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public interface IClaimsProvider
{
    public string ResourceType { get; set; }

    public Task<IEnumerable<Claim>> GetClaims(Permission permission);
}
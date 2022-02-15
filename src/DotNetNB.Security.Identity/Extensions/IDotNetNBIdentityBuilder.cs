using Microsoft.AspNetCore.Identity;

namespace DotNetNB.Security.Identity.Extensions;

public interface IDotNetNBIdentityBuilder
{
    IdentityBuilder IdentityBuilder { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace DotNetNB.Security.Identity.Extensions;

public class DotNetNBIdentityBuilder: IDotNetNBIdentityBuilder
{
    public IdentityBuilder IdentityBuilder { get; set; }
}
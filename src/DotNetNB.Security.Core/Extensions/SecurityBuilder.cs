using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Core.Extensions;

public class SecurityBuilder: ISecurityBuilder
{
    public IServiceCollection Services { get; set; }
}
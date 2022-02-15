using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.Core.Extensions;

public interface ISecurityBuilder
{
    public IServiceCollection Services { get; set; }
}
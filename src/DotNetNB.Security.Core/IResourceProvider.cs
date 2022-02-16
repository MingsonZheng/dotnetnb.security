using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.Core;

public interface IResourceProvider
{
    public Task<IEnumerable<Resource>> ExecuteAsync();
}
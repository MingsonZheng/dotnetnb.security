using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.EntityAccess
{
    public class EntityResourceProvider : IResourceProvider
    {
        public async Task<IEnumerable<Resource>> ExecuteAsync()
        {
            return new List<Resource>();
        }
    }
}

using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNB.Security.EntityAccess
{
    public static class SecurityOptionExtensions
    {
        public static SecurityOption AddEntityAccessControl(this SecurityOption option)
        {
            option.Services.TryAddEnumerable(ServiceDescriptor.Transient<IResourceProvider, EntityResourceProvider>());
            return option;
        }
    }
}

using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNB.Security.ActionAccess
{
    public static class SecurityOptionExtensions
    {
        public static SecurityOption AddActionAccessControl(this SecurityOption option)
        {
            option.Services.TryAddEnumerable(ServiceDescriptor.Transient<IResourceProvider, ActionResourceProvider>());
            return option;
        }
    }
}

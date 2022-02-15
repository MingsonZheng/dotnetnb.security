using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

namespace DotNetNB.Security.ActionAccess;

public static class ControllerActionDescriptorExtensions
{
    public static string GetPermissionKey(this ControllerActionDescriptor descriptor)
    {
        var httpMethod =
            (descriptor?.EndpointMetadata).FirstOrDefault(m => m is HttpMethodMetadata) as HttpMethodMetadata;

        return string.Format($"{descriptor?.ControllerName}-{descriptor?.ActionName}-{httpMethod.HttpMethods.First()}");
    }

}
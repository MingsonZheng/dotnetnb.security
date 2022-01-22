using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace DotNetNB.Security.ActionAccess;

public static class ControllerActionDescriptorExtensions
{
    public static string GetSecurityKey(this ControllerActionDescriptor descriptor)
    {
        var httpMethod = descriptor?.ActionConstraints.FirstOrDefault(c => c is HttpMethodActionConstraint) as HttpMethodActionConstraint;

        return string.Format($"{descriptor?.ControllerName}-{descriptor?.ActionName}-{httpMethod.HttpMethods.First()}");
    }
}
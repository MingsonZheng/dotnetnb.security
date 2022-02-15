using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DotNetNB.Security.ActionAccess;

public class DotNetNBAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        var endpoint = context.GetEndpoint();
        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor != null)
        {
            var permissions = context.User.Claims.Where(c => c.Type == Core.ClaimsTypes.Permission);
            var permissionKey = actionDescriptor.GetPermissionKey();
            var values = permissions.Select(p => p.Value);
            if (!values.Contains(permissionKey))
            {

                await context.ForbidAsync();
                return;
            }
        }
        
        if (authorizeResult.Challenged)
        {
            if (policy.AuthenticationSchemes.Count > 0)
            {
                foreach (var scheme in policy.AuthenticationSchemes)
                {
                    await context.ChallengeAsync(scheme);
                }
            }
            else
            {
                await context.ChallengeAsync();
            }

            return;
        }
        else if (authorizeResult.Forbidden)
        {
            if (policy.AuthenticationSchemes.Count > 0)
            {
                foreach (var scheme in policy.AuthenticationSchemes)
                {
                    await context.ForbidAsync(scheme);
                }
            }
            else
            {
                await context.ForbidAsync();
            }

            return;
        }
        
        await next(context);
    }
}

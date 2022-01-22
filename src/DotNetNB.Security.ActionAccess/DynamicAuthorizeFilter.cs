using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetNB.Security.ActionAccess
{
    public class DynamicAuthorizeFilter : AuthorizeFilter
    {
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor == null)
            {
                return;
            }

            base.OnAuthorizationAsync(context);
            if (context.Result != null)
            {
                return;
            }

            var permissions = context.HttpContext.User.Claims.Where(c => c.Type == Core.ClaimsTypes.Permission);
            var actionKey = actionDescriptor.GetSecurityKey();

            var values = permissions.Select(p => p.Value);
            if (!values.Contains(actionKey))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

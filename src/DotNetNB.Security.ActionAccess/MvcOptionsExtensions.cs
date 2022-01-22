using Microsoft.AspNetCore.Mvc;

namespace DotNetNB.Security.ActionAccess
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions AddActionAccessControl(this MvcOptions options)
        {
            options.Filters.Add<DynamicAuthorizeFilter>();
            return options;
        }
    }
}

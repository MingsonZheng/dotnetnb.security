using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;

namespace DotNetNB.Security.ActionAccess
{
    public class ActionResourceProvider : IResourceProvider
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public ActionResourceProvider(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public async Task<IEnumerable<Resource>> ExecuteAsync()
        {
            var actions = _actionDescriptorCollectionProvider.ActionDescriptors.Items;
            var actionResources = new List<ActionResource>();

            foreach (var action in actions)
            {
                if (action is ControllerActionDescriptor)
                {
                    var actionDescriptor = action as ControllerActionDescriptor;
                    var httpMethod = action.EndpointMetadata.Where(m => m is HttpMethodMetadata).FirstOrDefault() as HttpMethodMetadata;

                    var routeAttribute =
                        actionDescriptor?.EndpointMetadata.FirstOrDefault(m => m is RouteAttribute) as RouteAttribute;

                    var resourceData = new ActionResourceData();
                    resourceData.HttpVerb = httpMethod?.HttpMethods.First();
                    resourceData.ActionName = actionDescriptor?.ActionName;
                    resourceData.ControllerName = actionDescriptor?.ControllerName;
                    resourceData.RouteTemplate = routeAttribute?.Template;
                    resourceData.DisplayName = action.DisplayName;

                    actionResources.Add(new ActionResource()
                    {
                        Data = resourceData,
                        Key = actionDescriptor.GetSecurityKey()
                    });
                }
            }

            return await Task.FromResult(actionResources);
        }
    }
}
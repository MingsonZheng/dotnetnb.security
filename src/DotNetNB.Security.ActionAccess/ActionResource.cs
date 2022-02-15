using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.ActionAccess;

public class ActionResource : Resource
{
    public ActionResource()
    {
        Type = "Action";
    }
}

public class ActionResourceData
{
    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public string DisplayName { get; set; }

    public string? RouteTemplate { get; set; }

    public string? HttpVerb { get; set; }
}
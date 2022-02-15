using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.EntityAccess;

public class EntityResource : Resource
{
    public EntityResource()
    {
        Type = "Entity";
    }
}

public class EntityResourceData
{
    public IEnumerable<EntityMemberResource> Members { get; set; }
}

public class EntityMemberResource
{
    public string Name { get; set; }
}
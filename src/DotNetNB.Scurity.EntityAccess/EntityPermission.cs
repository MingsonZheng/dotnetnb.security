using DotNetNB.Security.Core.Models;

namespace DotNetNB.Security.EntityAccess;

public class EntityPermission : Permission<EntityPermissionData>
{
    public EntityPermission(Permission p)
    {
        this.Data = p.Data as EntityPermissionData;
        this.Key = p.Key;
        this.Description = p.Description;
        this.Group = p.Group;
        this.Resources = p.Resources;
        this.DisplayName = p.DisplayName;
    }
}
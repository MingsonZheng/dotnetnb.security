namespace DotNetNB.Security.EntityAccess;

public class EntityPermissionData
{
    public string EntityName { get; set; }

    public bool Create { get; set; }

    public bool Delete { get; set; }

    public IEnumerable<EntityMemberPermissionData> Members { get; set; }
}

public class EntityMemberPermissionData
{
    public string MemberName { get; set; }

    public bool Update { get; set; }
}
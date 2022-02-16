using DotNetNB.Security.EntityAccess;

namespace DotNetNB.WebApplication.ViewModels;

public class CreateEntityPermissionRequest: CreatePermissionRequest
{
    public EntityPermissionData Data { get; set; }
}
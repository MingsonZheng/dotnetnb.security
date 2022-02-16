using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;
using DotNetNB.Security.EntityAccess;
using DotNetNB.Security.Identity;
using DotNetNB.WebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNB.WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionManager _permissionManager;
    private readonly IRolePermissionManager<IdentityRole> _rolePermission;
    private readonly IUserPermissionManager<IdentityUser> _userPermission;

    public PermissionController(IPermissionManager permissionManager,
        IRolePermissionManager<IdentityRole> rolePermission,
        IUserPermissionManager<IdentityUser> userPermission)
    {
        _permissionManager = permissionManager;
        _rolePermission = rolePermission;
        _userPermission = userPermission;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _permissionManager.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request)
    {
        await _permissionManager.CreateAsync(request.Key, request.Group, request.DisplayName, request.Description, request.resources);
        return Ok();
    }

    [Route("entity")]
    [HttpPost]
    public async Task<IActionResult> CreateEntityPermission([FromBody] CreateEntityPermissionRequest request)
    {
        var permission = new Permission()
        {
            Data = request.Data,
            Description = request.Description,
            DisplayName = request.DisplayName,
            Key = request.Key,
            Group = request.Group,
            Resources = request.resources.Select(r => new EntityResource() { Key = r })
        };

        await _permissionManager.CreateAsync(permission);
        return Ok();
    }

    [Route("user/{username}")]
    [HttpGet]
    public async Task<IActionResult> FindUserPermission(string username)
    {
        return Ok(await _userPermission.FindUserPermission(username));
    }

    [Route("role/{roleName}")]
    [HttpGet]
    public async Task<IActionResult> FindRolePermission(string roleName)
    {
        return Ok(await _rolePermission.FindRolePermission(roleName));
    }

    [Route("addtorole")]
    [HttpPost]
    public async Task<IActionResult> AddToRole([FromQuery] string role, [FromQuery] string permission)
    {
        await _rolePermission.AddRolePermission(role, permission);
        return Ok();
    }

    [Route("addtouser")]
    [HttpPost]
    public async Task<IActionResult> AddToUser([FromQuery] string username, [FromQuery] string permission)
    {
        await _userPermission.AddUserPermission(username, permission);
        return Ok();
    }
}
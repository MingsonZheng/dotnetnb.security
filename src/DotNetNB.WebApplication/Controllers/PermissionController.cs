using DotNetNB.Security.Core;
using DotNetNB.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNB.WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionManager _permissionManager;

        public PermissionController(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _permissionManager.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request)
        {
            //await _permissionManager.CreateAsync(request.Key, request.DisplayName, request.Description, request.resources);
            return Ok();
        }
    }
}
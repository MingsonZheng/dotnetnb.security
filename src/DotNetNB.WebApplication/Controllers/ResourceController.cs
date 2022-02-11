using DotNetNB.Security.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNB.WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceManager _resourceManager;

        public ResourceController(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _resourceManager.GetAllAsync());
        }
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetNB.WebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetNB.WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController: ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    [HttpPost]  
    [Route("login")]  
    public async Task<IActionResult> Login([FromBody] LoginRequest.LoginModel model)  
    {  
        var user = await _userManager.FindByNameAsync(model.Username);
        var userClaims = await _userManager.GetClaimsAsync(user);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))  
        {  
            var userRoles = await _userManager.GetRolesAsync(user);  
  
            var authClaims = new List<Claim>  
            {  
                new Claim(ClaimTypes.Name, user.UserName),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            };  
  
            foreach (var userRole in userRoles)  
            {  
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                authClaims.AddRange(roleClaims);
            }

            authClaims.AddRange(userClaims);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
            var token = new JwtSecurityToken(  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
            );  
  
            return Ok(new  
            {  
                token = new JwtSecurityTokenHandler().WriteToken(token),  
                expiration = token.ValidTo  
            });  
        }  
        return Unauthorized();  
    }  
    
    [HttpPost]  
    [Route("register")]  
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)  
    {  
        var userExists = await _userManager.FindByNameAsync(model.Username);  
        if (userExists != null)  
            return StatusCode(StatusCodes.Status500InternalServerError, "User already exist");  
  
        var user = new IdentityUser()  
        {  
            Email = model.Email,  
            SecurityStamp = Guid.NewGuid().ToString(),  
            UserName = model.Username  
        };  
        var result = await _userManager.CreateAsync(user, model.Password);  
        if (!result.Succeeded)  
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);  
  
        return Ok();  
    }  
    
    
}
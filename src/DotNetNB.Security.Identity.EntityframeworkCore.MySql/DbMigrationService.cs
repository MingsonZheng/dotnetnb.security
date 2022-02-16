using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace DotNetNB.Security.Identity.EntityframeworkCore.MySql;

public class DbMigrationService: IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    public DbMigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async Task EnsureDatabaseMigratedAsync(IServiceProvider serviceProvider)
    {
        using (var scope  = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DotNetNBIdentityDbContext>();
            await context.Database.MigrateAsync();
            
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var identityData = scope.ServiceProvider.GetRequiredService<IOptions<IdentityData>>();

            await EnsureSeedIdentityDataAsync(userManager, roleManager, identityData.Value);
        }
    }

    private static async Task EnsureSeedIdentityDataAsync(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, IdentityData identityData)
    {
             // adding roles from seed
                    foreach (var r in identityData.Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(r.Name))
                        {
                            var role = new IdentityRole()
                            {
                                Name = r.Name
                            };
        
                            var result = await roleManager.CreateAsync(role);
        
                            if (result.Succeeded)
                            {
                                foreach (var claim in r.Claims)
                                {
                                    await roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                                }
                            }
                        }
                    }
                    foreach (var user in identityData.Users)
                    {
                        var identity = new IdentityUser()
                        {
                            UserName = user.Username,
                            Email = user.Email,
                            EmailConfirmed = true,
                        };
        
                        var userByUsername = await userManager.FindByNameAsync(identity.UserName);
                        var userByEmail = await userManager.FindByEmailAsync(identity.Email);
                        
                        if(userByEmail!=null || userByEmail!=null)
                            continue;
                        
                        var result = !string.IsNullOrEmpty(user.Password)
                            ? await userManager.CreateAsync(identity, user.Password)
                            : await userManager.CreateAsync(identity);
        
                        if (result.Succeeded)
                        {
                            foreach (var claim in user.Claims)
                            {
                                await userManager.AddClaimAsync(identity, new Claim(claim.Type, claim.Value));
                            }
        
                            foreach (var role in user.Roles)
                            {
                                await userManager.AddToRoleAsync(identity, role);
                            }
                        }
                    }

    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await EnsureDatabaseMigratedAsync(_serviceProvider);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}
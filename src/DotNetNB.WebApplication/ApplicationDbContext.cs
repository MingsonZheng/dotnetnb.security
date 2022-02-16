using DotNetNB.Security.EntityAccess;
using DotNetNB.WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.WebApplication;

public class ApplicationDbContext: SecurityDbContext
{
    public ApplicationDbContext(IServiceProvider serviceProvider, DbContextOptions<ApplicationDbContext> options) : base(serviceProvider, options)
    {
        
    }
    
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
}
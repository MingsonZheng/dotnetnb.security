using Microsoft.EntityFrameworkCore;

namespace DotNetNB.Security.EntityAccess;

public class SecurityDbContext:DbContext
{
    private readonly IServiceProvider _serviceProvider;
    public SecurityDbContext(IServiceProvider serviceProvider, DbContextOptions options) : base(options)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(new SavingChangeInterceptor(_serviceProvider));
}
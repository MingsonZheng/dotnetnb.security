using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.EntityAccess.Extensions;

public interface IEntityAccessBuilder
{
    IServiceCollection Services { get; set; }

    public List<Type> DbContexts { get; set; }
}
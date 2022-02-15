using DotNetNB.Security.EntityAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.EntityAccess;

public class EntityAccessBuilder: IEntityAccessBuilder
{
    public IServiceCollection Services { get; set; }

    public List<Type> DbContexts { get; set; } = new List<Type>();
}
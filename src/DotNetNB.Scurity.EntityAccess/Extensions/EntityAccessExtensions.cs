namespace DotNetNB.Security.EntityAccess.Extensions;

public static class EntityAccessExtensions
{
    public static IEntityAccessBuilder AddDbContext<TContext>(this IEntityAccessBuilder builder)
    where TContext: SecurityDbContext
    {
        builder.DbContexts.Add(typeof(TContext));
        return builder;
    }
}
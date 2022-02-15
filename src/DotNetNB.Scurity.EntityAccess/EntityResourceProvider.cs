using DotNetNB.Security.Core;
using DotNetNB.Security.Core.Models;
using DotNetNB.Security.EntityAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DotNetNB.Security.EntityAccess;

public class EntityResourceProvider : IResourceProvider
{
    private readonly IEntityAccessBuilder _entityAccessBuilder;
    private readonly IServiceProvider _serviceProvider;

    public EntityResourceProvider(IEntityAccessBuilder entityAccessBuilder, IServiceProvider serviceProvider)
    {
        _entityAccessBuilder = entityAccessBuilder;
        _serviceProvider = serviceProvider;
    }
    public async Task<IEnumerable<Resource>> ExecuteAsync()
    {
        var dbContexts = _entityAccessBuilder.DbContexts;
        var resources = new List<Resource>();
        foreach (var dbContext in dbContexts)
        {
            var context = _serviceProvider.GetService(dbContext) as DbContext;
            var models = context.Model.GetEntityTypes();


            foreach (var model in models)
            {
                var resource = new EntityResource() { Key = model.Name, Group = dbContext.Name };
                var properties = model.GetProperties();

                resource.Data = new EntityResourceData()
                {
                    Members = properties.Select(p => new EntityMemberResource() { Name = p.Name })
                };
                resources.Add(resource);
            }
        }

        return resources;
    }
}
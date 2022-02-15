using DotNetNB.Security.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNB.Security.EntityAccess;

public class SavingChangeInterceptor: ISaveChangesInterceptor
{
    
    private readonly IPermissionManager? _permissionManager;
    private readonly IHttpContextAccessor? _contextAccessor;
    
    public SavingChangeInterceptor(IServiceProvider serviceProvider)
    {
        _permissionManager = serviceProvider.GetService<IPermissionManager>();
        _contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    }
    
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        return result;
    }

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        return result;
    }

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        
    }

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var contextName = eventData.Context.GetType().Name;
        var permissions = await _permissionManager.GetByGroupAsync(contextName);
        var entityPermissions = permissions.Select(p => new EntityPermission(p)).ToList();

        
        if (permissions==null || !permissions.Any())
            return result;

        var addedEntities = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
        var deletedEntties = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
        var modifiedEntities = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

        await CheckAddedEntitiesAsync(addedEntities, entityPermissions);
        await CheckDeletedEntitiesAsync(deletedEntties,entityPermissions);
        await CheckModifiedEntitiesAsync(modifiedEntities,entityPermissions);
        
        return result;
    }
    
    private async Task CheckAddedEntitiesAsync(IEnumerable<EntityEntry> entities,IEnumerable<EntityPermission> permissions)
    {
        foreach (var entity in entities)
        {
            var entityName = entity.Metadata.Name;
            var entityPermissions = permissions.Where(p => p.Data.EntityName == entityName);
            
            if(!entityPermissions.Any())
                continue;

            var user = _contextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException();

            var createPermission = entityPermissions.Where(e => e.Data.Create).Select(e => e.Key);
            var claimValues = user.Claims.Where(c => c.Type == ClaimsTypes.Permission).Select(c => c.Value);
            if (!createPermission.Intersect(claimValues).Any())
                throw new AuthorizationException();

        }
    }

    private async Task CheckDeletedEntitiesAsync(IEnumerable<EntityEntry> entities,IEnumerable<EntityPermission> permissions)
    {
        foreach (var entity in entities)
        {
            var entityName = entity.Metadata.Name;
            var entityPermissions = permissions.Where(p => p.Data.EntityName == entityName);
            
            if(!entityPermissions.Any())
                continue;

            var user = _contextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException();

            var deletePermission = entityPermissions.Where(e => e.Data.Delete).Select(e => e.Key);
            var claimValues = user.Claims.Where(c => c.Type == ClaimsTypes.Permission).Select(c => c.Value);
            if (!deletePermission.Intersect(claimValues).Any())
                throw new AuthorizationException();

        }
    }

    private async Task CheckModifiedEntitiesAsync(IEnumerable<EntityEntry> entities,IEnumerable<EntityPermission> permissions)
    {
        foreach (var entity in entities)
        {
            var entityName = entity.Metadata.Name;
            var entityPermissions = permissions.Where(p => p.Data.EntityName == entityName);
            
            if(!entityPermissions.Any())
                continue;

            var user = _contextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new AuthenticationException();
            
            var modifiedMembers = entity.Members.Where(m => m.IsModified).Select(m => m.Metadata.Name);
            var canUpdatePermissionKeys = new List<string>();

            foreach (var permission in entityPermissions)
            {
                var definedMembers = permission.Data.Members.Where(m => m.Update && modifiedMembers.Contains(m.MemberName));
                if(definedMembers.Any())
                    canUpdatePermissionKeys.Add((permission.Key));
            }

            var claimValues = user.Claims.Where(c => c.Type == ClaimsTypes.Permission).Select(c => c.Value);
            if (!canUpdatePermissionKeys.Intersect(claimValues).Any())
                throw new AuthorizationException();
        }
    }

    public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return result;
    }

    public async Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
        CancellationToken cancellationToken = new CancellationToken())
    {
        
    }
}
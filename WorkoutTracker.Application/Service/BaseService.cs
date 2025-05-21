using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class BaseService<T>(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext) : IScopedService
    where T : BaseEntity
{
    public async Task<T> Add(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateCreated = DateTime.UtcNow;
        entity.DateUpdated = DateTime.UtcNow;
        entity.IsDeleted = false;
        if (typeof(T).IsAssignableTo(typeof(IHasUser)))
        {
            ((IHasUser)entity).User = userContext.User;
        }
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task AddRange(List<T> entities)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        foreach (var entity in entities)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;
            entity.IsDeleted = false; 
            if (typeof(T).IsAssignableTo(typeof(IHasUser)))
            {
                ((IHasUser)entity).User = userContext.User;
            }
        }
        
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateUpdated = DateTime.UtcNow;
        context.Set<T>().Update(entity);
        var type = typeof(T);
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            var accessor = type.GetProperty(property.Name)?.GetGetMethod();
            if (accessor?.IsVirtual != true)
            {
                continue;
            }
            var value = property.Name;
            var prop = type.GetProperty(value);
            context.Entry(entity).Property(value).IsModified = false;
            // var value = property.GetValue(entity);
            // if (value != null)
            // {
            //     context.Entry(value).State = EntityState.Detached;
            // }
        }
        await context.SaveChangesAsync();
    }
    
    public async Task<List<T>> Get(Expression<Func<T, bool>>? predicate = null)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<T>().AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (typeof(T).IsAssignableTo(typeof(IHasUser)))
        {
            query = query.Where(x => ((IHasUser)x).UserId == userContext.Id);
        }
        query = query.Where(x => x.IsDeleted == false).OrderByDescending(x => x.DateUpdated);
        var result = await query.ToListAsync();
        return result;
    }
    
    public async Task<T?> Get(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<T>().AsQueryable();
        if (typeof(T).IsAssignableTo(typeof(IHasUser)))
        {
            query = query.Where(x => ((IHasUser)x).UserId == userContext.Id);
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }
    
    public async Task Remove(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.IsDeleted = true;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
}
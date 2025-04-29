using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class BaseService<T>(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : IScopedService<T>
    where T : BaseEntity
{
    public async Task Add(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateCreated = DateTime.UtcNow;
        entity.DateUpdated = DateTime.UtcNow;
        entity.IsDeleted = false;
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateUpdated = DateTime.UtcNow;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<T>> Get(Expression<Func<T, bool>>? predicate = null)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        if (predicate == null)
        {
            return await context.Set<T>().Where(x => x.IsDeleted == false).OrderByDescending(x => x.DateUpdated).ToListAsync();
        }
        return await context.Set<T>().Where(x => x.IsDeleted == false).Where(predicate).OrderByDescending(x => x.DateUpdated).ToListAsync();
    }
    
    public async Task<T?> Get(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
    }
    
    public async Task Remove(T entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.IsDeleted = true;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
}
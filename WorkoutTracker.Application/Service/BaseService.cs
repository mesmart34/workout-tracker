using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Contracts;
using WorkoutTracker.Infrastructure.Db;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Service;

public class BaseService<TTable, TService>(
    IDbContextFactory<WorkoutTrackerDbContext> contextFactory,
    IUserContext userContext,
    IMapper mapper) : IScopedService<TService>
    where TService : BaseEntity
    where TTable : BaseTableEntity
{
    public async Task<TService> Add(TService entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var mapped = mapper.Map<TService, TTable>(entity);
        mapped.DateCreated = DateTime.UtcNow;
        mapped.DateUpdated = DateTime.UtcNow;
        mapped.IsDeleted = false;
        if (typeof(TTable).IsAssignableTo(typeof(IHasTableUser)))
        {
            ((IHasTableUser)mapped).UserId = userContext.User.Id;
        }
        await context.Set<TTable>().AddAsync(mapped);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task AddRange(List<TService> entities)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var mapped = mapper.Map<List<TService>, List<TTable>>(entities);
        foreach (var entity in mapped)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;
            entity.IsDeleted = false; 
            if (typeof(TService).IsAssignableTo(typeof(IHasTableUser)))
            {
                ((IHasTableUser)entity).UserId = userContext.User.Id;
            }
        }
        await context.Set<TTable>().AddRangeAsync(mapped);
        await context.SaveChangesAsync();
    }

    public async Task Update(TService entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateUpdated = DateTime.UtcNow;
        var mapped = mapper.Map<TService, TTable>(entity);
        context.Set<TTable>().Update(mapped);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<TService>> Get(Expression<Func<TService, bool>>? predicate = null, bool ignoreDeletion = false)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<TTable>().AsQueryable();
        
        if (predicate != null)
        {
            query = query.Where(mapper.MapExpression<Expression<Func<TService, bool>>, Expression<Func<TTable, bool>>>(predicate));
        }

        if (typeof(TTable).IsAssignableTo(typeof(IHasTableUser)))
        {
            query = query.Where(x => ((IHasTableUser)x).UserId == userContext.Id);
        }

        if (!ignoreDeletion)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        
        query = query.OrderByDescending(x => x.DateUpdated);
        var result = await query.ToListAsync();
        return mapper.Map<List<TTable>, List<TService>>(result);
    }
    
    public async Task<TService?> Get(Guid id, bool ignoreDeletion = false)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<TTable>().AsQueryable();
        if (typeof(TTable).IsAssignableTo(typeof(IHasTableUser)))
        {
            query = query.Where(x => ((IHasTableUser)x).UserId == userContext.Id);
        }
        
        if (!ignoreDeletion)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        
        var result =  await query.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        if (result == null)
        {
            return null;
        }
        var mapped = mapper.Map<TTable, TService>(result);
        return mapped;
    }
    
    public async Task Remove(TService entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var mapped = mapper.Map<TService, TTable>(entity);
        mapped.IsDeleted = true;
        context.Set<TTable>().Update(mapped);
        await context.SaveChangesAsync();
    }
}
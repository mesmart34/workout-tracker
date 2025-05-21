using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Contracts;
using WorkoutTracker.Infrastructure.Db;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Service;

public class BaseService<TTable, TService>(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext, IMapper mapper) : IScopedService<TService>
    where TService : BaseEntity
    where TTable : BaseTableEntity
{
    public async Task<TService> Add(TService entity)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        entity.DateCreated = DateTime.UtcNow;
        entity.DateUpdated = DateTime.UtcNow;
        entity.IsDeleted = false;
        if (typeof(TService).IsAssignableTo(typeof(IHasUser)))
        {
            ((IHasUser)entity).User = userContext.User;
        }
        await context.Set<TService>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    
    public async Task AddRange(List<TService> entities)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        foreach (var entity in entities)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateUpdated = DateTime.UtcNow;
            entity.IsDeleted = false; 
            if (typeof(TService).IsAssignableTo(typeof(IHasUser)))
            {
                ((IHasUser)entity).User = userContext.User;
            }
        }
        
        await context.Set<TService>().AddRangeAsync(entities);
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
    
    public async Task<List<TService>> Get(Expression<Func<TService, bool>>? predicate = null)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<TTable>().AsQueryable();
        if (predicate != null)
        {
            var pred = mapper.MapExpression<Expression<Func<TService, bool>>, Expression<Func<TTable, bool>>>(predicate);
            query = query.Where(pred);
        }

        if (typeof(TTable).IsAssignableTo(typeof(IHasTableUser)))
        {
            query = query.Where(x => ((IHasTableUser)x).UserId == userContext.Id);
        }
        query = query.Where(x => x.IsDeleted == false).OrderByDescending(x => x.DateUpdated);
        var result = await query.ToListAsync();
        return result.Select(mapper.Map<TTable, TService>).ToList();
    }
    
    public async Task<TService?> Get(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Set<TTable>().AsQueryable();
        if (typeof(TTable).IsAssignableTo(typeof(IHasTableUser)))
        {
            query = query.Where(x => ((IHasTableUser)x).UserId == userContext.Id);
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
        entity.IsDeleted = true;
        context.Set<TService>().Update(entity);
        await context.SaveChangesAsync();
    }
}
using System.Linq.Expressions;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Application.Contracts;

public interface IScopedService<TService> where TService : BaseEntity
{
    public Task<TService> Add(TService entity);

    public Task AddRange(List<TService> entities);

    public Task Update(TService entity);
    
    public  Task<List<TService>> Get(Expression<Func<TService, bool>>? predicate = null, bool ignoreDeletion = false);

    public Task<TService?> Get(Guid id, bool ignoreDeletion = false);
    
    public Task Remove(TService entity);
}
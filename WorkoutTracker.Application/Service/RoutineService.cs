using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class RoutineService : BaseService<RoutineEntity>
{
    public RoutineService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : base(contextFactory)
    {
    }
}
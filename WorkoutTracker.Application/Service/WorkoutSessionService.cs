using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class WorkoutSessionService : BaseService<WorkoutSessionEntity>
{
    public WorkoutSessionService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : base(contextFactory)
    {
    }
}
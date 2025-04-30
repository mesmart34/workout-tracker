using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class ExerciseService : BaseService<ExerciseEntity>
{
    public ExerciseService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : base(contextFactory)
    {
    }
}
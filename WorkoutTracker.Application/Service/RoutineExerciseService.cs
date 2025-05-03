using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class RoutineExerciseService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : BaseService<RoutineExerciseEntity>(contextFactory)
{
    
}
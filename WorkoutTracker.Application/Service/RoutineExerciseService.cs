using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Service;

public class RoutineExerciseService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext, IMapper mapper) : BaseService<TableRoutineExerciseEntity, RoutineExerciseEntity>(contextFactory, userContext, mapper)
{
    
}
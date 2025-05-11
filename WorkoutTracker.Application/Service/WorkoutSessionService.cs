using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class WorkoutSessionService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext) : BaseService<WorkoutSessionEntity>(contextFactory, userContext)
{
    
}
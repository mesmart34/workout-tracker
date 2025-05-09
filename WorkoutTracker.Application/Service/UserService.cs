using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class UserService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : BaseService<UserEntity>(contextFactory)
{
    
}
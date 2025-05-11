using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class UserService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext) : BaseService<UserEntity>(contextFactory, userContext)
{
    
}
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class EquipmentService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory, IUserContext userContext) : BaseService<EquipmentEntity>(contextFactory, userContext)
{
    
}
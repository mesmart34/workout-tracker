using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker.Application.Service;

public class EquipmentService : BaseService<EquipmentEntity>
{
    public EquipmentService(IDbContextFactory<WorkoutTrackerDbContext> contextFactory) : base(contextFactory)
    {
    }
}
using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableEquipmentEntity : BaseTableNamedEntity, IHasTableUser
{
    public Guid UserId { get; set; }
    
    public virtual TableUserEntity User { get; set; } = null!;
}
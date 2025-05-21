using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableRoutineEntity : BaseTableNamedEntity, IHasTableUser
{
    public virtual List<TableRoutineExerciseEntity> RoutineExercises { get; set; } = new();
    
    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
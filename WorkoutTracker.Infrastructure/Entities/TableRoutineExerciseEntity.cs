using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableRoutineExerciseEntity : BaseTableEntity, IHasTableUser
{
    public Guid RoutineId { get; set; }
    
    public virtual TableRoutineEntity Routine { get; set; } = null!;
    
    public Guid? ExerciseId { get; set; }
    
    public virtual TableExerciseEntity? Exercise { get; set; }

    public string Notes { get; set; } = string.Empty;
    
    public int Order { get; set; }
    
    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
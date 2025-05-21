using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableWorkoutSessionExerciseEntity : BaseTableEntity, IHasTableUser
{
    public Guid WorkoutSessionId { get; set; }

    public virtual TableWorkoutSessionEntity WorkoutSession { get; set; } = null!;
    
    public virtual List<TableSetEntity> Sets { get; set; } = new();
    
    public Guid ExerciseId { get; set; }

    public virtual TableExerciseEntity Exercise { get; set; } = null!;
    
    public int Order { get; set; }
    
    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableSetEntity : BaseTableEntity, IHasTableUser
{
    public Guid WorkoutSessionExerciseId { get; set; }
    
    public virtual TableWorkoutSessionExerciseEntity WorkoutSessionExerciseEntity { get; set; } = null!;
    
    public int Reps { get; set; }
    
    public int Order { get; set; }
    
    public float Weight { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
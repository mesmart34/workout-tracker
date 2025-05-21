namespace WorkoutTracker.Domain.Entities;

public class SetEntity : BaseEntity, IHasUser
{
    public Guid WorkoutSessionExerciseId { get; set; }
    
    public virtual WorkoutSessionExerciseEntity WorkoutSessionExerciseEntity { get; set; } = null!;
    
    public int Reps { get; set; }
    
    public int Order { get; set; }
    
    public float Weight { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}
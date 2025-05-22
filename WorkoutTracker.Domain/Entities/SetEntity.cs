namespace WorkoutTracker.Domain.Entities;

public class SetEntity : BaseEntity, IHasUser
{
    public virtual WorkoutSessionExerciseEntity WorkoutSessionExerciseEntity { get; set; } = null!;
    
    public int Reps { get; set; }
    
    public int Order { get; set; }
    
    public float Weight { get; set; }
    
    public TimeSpan? Duration { get; set; }

    public UserEntity User { get; set; } = null!;
}
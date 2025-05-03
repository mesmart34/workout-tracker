namespace WorkoutTracker.Domain.Entities;

public class SetEntity : BaseEntity
{
    public Guid WorkoutSessionId { get; set; }
    
    public virtual WorkoutSessionEntity WorkoutSession { get; set; } = null!;
    
    public Guid ExerciseId { get; set; }
    
    public virtual ExerciseEntity Exercise { get; set; } = null!;
    
    public int Reps { get; set; }
    
    public float Weight { get; set; }
    
    public TimeSpan? Duration { get; set; }
}
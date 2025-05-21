namespace WorkoutTracker.Domain.Entities;

public class WorkoutSessionExerciseEntity : BaseEntity, IHasUser
{
    public Guid WorkoutSessionId { get; set; }

    public virtual WorkoutSessionEntity WorkoutSession { get; set; } = null!;
    
    public virtual List<SetEntity> Sets { get; set; } = new();
    
    public Guid ExerciseId { get; set; }

    public virtual ExerciseEntity Exercise { get; set; } = null!;
    
    public int Order { get; set; }
    
    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}
using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class WorkoutSessionEntity : BaseEntity, IHasUser
{
    public virtual List<WorkoutSessionExerciseEntity> Exercises { get; set; } = new();
    
    public DateTime WorkoutDate { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public Mood Mood { get; set; } = Mood.Ok;

    public string Notes { get; set; } = string.Empty;
    
    public Guid RoutineId { get; set; }
    
    public virtual RoutineEntity? Routine { get; set; }
    
    public bool Complete { get; set; }

    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}
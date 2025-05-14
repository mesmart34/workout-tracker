using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class WorkoutSessionEntity : BaseEntity, IHasUser
{
    public DateTime WorkoutDate { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public virtual List<SetEntity> Sets { get; set; } = new();

    public Mood Mood { get; set; } = Mood.Ok;
    
    public Guid RoutineId { get; set; }
    public virtual RoutineEntity Routine { get; set; } = null!;

    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}
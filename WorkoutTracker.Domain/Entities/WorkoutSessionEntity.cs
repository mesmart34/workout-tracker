using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class WorkoutSessionEntity : BaseEntity
{
    public DateTime WorkoutDate { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public virtual List<SetEntity> Sets { get; set; } = null!; 
    
    public Mood Mood { get; set; }
    
}
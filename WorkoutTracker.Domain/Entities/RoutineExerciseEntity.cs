using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class RoutineExerciseEntity : BaseEntity, IHasUser
{
    public Guid RoutineId { get; set; }
    
    public virtual RoutineEntity Routine { get; set; } = null!;
    
    public Guid? ExerciseId { get; set; }
    
    public virtual ExerciseEntity? Exercise { get; set; }

    public string Notes { get; set; } = string.Empty;
    
    public int Order { get; set; }
    
    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}
using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class RoutineExerciseEntity : BaseEntity, IHasUser
{
    public virtual RoutineEntity Routine { get; set; } = null!;
    
    public virtual ExerciseEntity? Exercise { get; set; }

    public string Notes { get; set; } = string.Empty;
    
    public int Order { get; set; }

    public UserEntity User { get; set; } = null!;
}
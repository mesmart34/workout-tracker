namespace WorkoutTracker.Domain.Entities;

public class RoutineEntity : BaseNamedEntity, IHasUser
{
    public virtual List<RoutineExerciseEntity> RoutineExercises { get; set; } = new();

    public UserEntity User { get; set; } = null!;
}
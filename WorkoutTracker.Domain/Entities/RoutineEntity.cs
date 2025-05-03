namespace WorkoutTracker.Domain.Entities;

public class RoutineEntity : BaseNamedEntity
{
    public virtual List<RoutineExerciseEntity> RoutineExercises { get; set; } = new();
}
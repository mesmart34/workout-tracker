namespace WorkoutTracker.Domain.Entities;

public class RoutineEntity : BaseNamedEntity
{
    public virtual List<ExerciseEntity>? Exercises { get; set; }    
}
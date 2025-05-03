using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class ExerciseEntity : BaseNamedEntity
{
    public ExerciseType ExerciseType { get; set; }
    
    public MuscleGroup MuscleGroup { get; set; }
    
    public Guid? EquipmentId { get; set; }
    
    public virtual EquipmentEntity? Equipment { get; set; }

    public virtual List<RoutineExerciseEntity> RoutineExercises { get; set; } = new();

    //public virtual List<SetEntity> Sets { get; set; } = new();
}
using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class ExerciseEntity : BaseNamedEntity, IHasUser
{
    public ExerciseType ExerciseType { get; set; }
    
    public MuscleGroup MuscleGroup { get; set; }
    
    public virtual EquipmentEntity? Equipment { get; set; }

    public virtual List<RoutineExerciseEntity> RoutineExercises { get; set; } = new();

    public UserEntity User { get; set; } = null!;
}
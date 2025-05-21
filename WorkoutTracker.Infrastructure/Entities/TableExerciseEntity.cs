using WorkoutTracker.Domain.Common;
using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableExerciseEntity :  BaseTableNamedEntity, IHasTableUser
{
    public ExerciseType ExerciseType { get; set; }
    
    public MuscleGroup MuscleGroup { get; set; }
    
    public Guid? EquipmentId { get; set; }
    
    public virtual TableEquipmentEntity? Equipment { get; set; }

    public virtual List<TableRoutineExerciseEntity> RoutineExercises { get; set; } = new();
    
    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
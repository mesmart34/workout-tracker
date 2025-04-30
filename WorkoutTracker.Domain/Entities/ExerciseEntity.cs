using WorkoutTracker.Domain.Common;

namespace WorkoutTracker.Domain.Entities;

public class ExerciseEntity : BaseNamedEntity
{
    public Guid? EquipmentId { get; set; }
    public virtual EquipmentEntity? Equipment { get; set; }
    public ExerciseType? ExerciseType { get; set; }
}
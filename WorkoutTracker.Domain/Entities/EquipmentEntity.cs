namespace WorkoutTracker.Domain.Entities;

public class EquipmentEntity : BaseNamedEntity, IHasUser
{
    public UserEntity User { get; set; } = null!;
}
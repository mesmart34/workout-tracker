namespace WorkoutTracker.Domain.Entities;

public class EquipmentEntity : BaseNamedEntity, IHasUser
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}
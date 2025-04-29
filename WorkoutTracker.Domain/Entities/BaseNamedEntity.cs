namespace WorkoutTracker.Domain.Entities;

public abstract class BaseNamedEntity : BaseEntity
{
    public string? Name { get; set; }
}
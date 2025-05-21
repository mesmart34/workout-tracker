namespace WorkoutTracker.Infrastructure.Entities;

public class BaseTableEntity
{
    public Guid Id { get; set; }
    
    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public bool IsDeleted { get; set; } = false;
}
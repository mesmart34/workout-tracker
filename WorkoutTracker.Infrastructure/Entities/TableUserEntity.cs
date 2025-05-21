using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableUserEntity : BaseTableEntity
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public bool IsAdmin { get; set; }
}
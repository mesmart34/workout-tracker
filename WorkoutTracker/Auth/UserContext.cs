using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Auth;

public class UserContext : IUserContext
{
    public Guid Id { get; set; }

    public UserEntity User { get; set; } = null!;
    
    public string? Email { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}
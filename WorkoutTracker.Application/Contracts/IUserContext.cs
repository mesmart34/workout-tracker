using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Application.Contracts;

public interface IUserContext
{
    public Guid Id { get; set; }
    
    public UserEntity User { get; set; }
    
    public string? Email { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}
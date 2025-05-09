using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Domain;

public interface IHasUser
{
    public Guid UserId { get; set; }

    public UserEntity User { get; set; }
}
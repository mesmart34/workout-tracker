using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Domain;

public interface IHasUser
{

    public UserEntity User { get; set; }
}
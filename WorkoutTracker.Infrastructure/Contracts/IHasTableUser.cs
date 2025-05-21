using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Contracts;

public interface IHasTableUser
{
    public Guid UserId { get; set; }

    public TableUserEntity User { get; set; }
}
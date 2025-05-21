using WorkoutTracker.Domain.Common;
using WorkoutTracker.Infrastructure.Contracts;

namespace WorkoutTracker.Infrastructure.Entities;

public class TableWorkoutSessionEntity : BaseTableEntity, IHasTableUser
{
    public virtual List<TableWorkoutSessionExerciseEntity> Exercises { get; set; } = new();
    
    public DateTime WorkoutDate { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public Mood Mood { get; set; } = Mood.Ok;

    public string Notes { get; set; } = string.Empty;
    
    public Guid RoutineId { get; set; }
    
    public virtual TableRoutineEntity? Routine { get; set; }
    
    public bool Complete { get; set; }

    public Guid UserId { get; set; }

    public virtual TableUserEntity User { get; set; } = null!;
}
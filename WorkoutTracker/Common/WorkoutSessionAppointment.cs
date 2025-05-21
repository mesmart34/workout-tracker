namespace WorkoutTracker.Common;

public class WorkoutSessionAppointment
{
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public string WorkoutName { get; set; } = string.Empty;
    
    public Guid WorkoutSessionId { get; set; }
}
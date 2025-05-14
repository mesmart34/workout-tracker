using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Domain.Common;

public enum Mood
{
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.MoodPerfect))]
    Perfect = 0,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.MoodGood))]
    Good = 1,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.OkMood))]
    Ok = 2,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.MoodBad))]
    Bad = 3,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.MoodTerrible))]
    Terrible = 4
}
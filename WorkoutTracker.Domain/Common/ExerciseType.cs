using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Domain.Common;

public enum ExerciseType
{
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.ExerciseTypeWeight))]
    Weight = 1,
    
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.ExerciseTypeTime))]
    Time = 2
}
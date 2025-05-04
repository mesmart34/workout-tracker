using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Domain.Common;

public enum MuscleGroup
{
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.Biceps))]
    Biceps = 0,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.Biceps))]
    Spine = 1,
    [Display(ResourceType = typeof(Resource), Name = nameof(Resource.Biceps))]
    Chest = 2
}
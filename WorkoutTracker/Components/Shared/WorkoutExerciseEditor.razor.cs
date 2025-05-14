using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared;

public partial class WorkoutExerciseEditor : ComponentBase
{
    [Required]
    [Parameter]
    public RoutineExerciseEntity RoutineExercise { get; set; } = null!;

    protected override Task OnInitializedAsync()
    {
        
        return base.OnInitializedAsync();
    }
}
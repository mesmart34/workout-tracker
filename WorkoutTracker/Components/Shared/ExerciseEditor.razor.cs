using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared;

public partial class ExerciseEditor : ComponentBase
{
    private ExerciseEntity _model = new ();
    private IEnumerable<Enum>? _exerciseTypes;

    [Required] 
    [Parameter] 
    public ExerciseEntity Model { get; set; } = null!;
    
    [Inject]
    public DialogService DialogService { get; set; } = null!;

    protected override Task OnInitializedAsync()
    {
        _model.Name = Model.Name;
        _model.ExerciseType = Model.ExerciseType;
        _exerciseTypes = Enum.GetValues<ExerciseType>().Cast<Enum>();
        return base.OnInitializedAsync();
    }

    private void Submit(ExerciseEntity item)
    {
        Model.Name = _model.Name;
        Model.ExerciseType = _model.ExerciseType;
        DialogService.Close(true);
    }

    private void Close()
    {
        DialogService.Close(false);
    }
}
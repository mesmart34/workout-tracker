using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared.Dialogs;

public partial class ExerciseDialog : ComponentBase
{
    private ExerciseEntity _model = new ();
    private IEnumerable<Enum>? _exerciseTypes;
    private List<EquipmentEntity> _equipments = new();

    [Required] 
    [Parameter] 
    public ExerciseEntity Model { get; set; } = null!;
    
    [Inject]
    public DialogService DialogService { get; set; } = null!;
    
    [Inject]
    public EquipmentService EquipmentService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _model.Name = Model.Name;
        _model.ExerciseType = Model.ExerciseType;
        _model.MuscleGroup = Model.MuscleGroup;
        _model.Equipment = Model.Equipment;
        _exerciseTypes = Enum.GetValues<ExerciseType>().Cast<Enum>();
        _equipments = await EquipmentService.Get();
        await base.OnInitializedAsync();
    }

    private void Submit(ExerciseEntity item)
    {
        Model.Name = _model.Name;
        Model.ExerciseType = _model.ExerciseType;
        Model.MuscleGroup = _model.MuscleGroup;
        Model.Equipment = _model.Equipment;
        DialogService.Close(true);
    }

    private void Close()
    {
        DialogService.Close(false);
    }
}
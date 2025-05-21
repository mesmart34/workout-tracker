using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared.Dialogs;

public partial class RoutineDialog : ComponentBase
{
    private RoutineEntity _model = new();
    private List<EquipmentEntity> _equipments = new();
    private List<ExerciseEntity> _exercises = new();
    private List<RoutineExerciseEntity> _routineExerciseEntities = new();

    [Parameter]
    public Guid Id { get; set; }

    [Inject] 
    public DialogService DialogService { get; set; } = null!;
    
    [Inject]
    private RoutineService RoutineService { get; set; } = null!;
    
    [Inject]
    private RoutineExerciseService RoutineExerciseService { get; set; } = null!;
    
    [Inject]
    private ExerciseService ExerciseService { get; set; } = null!;
    
    [Inject]
    private EquipmentService EquipmentService { get; set; } = null!;
    
    [Inject] 
    public NavigationManager NavigationManager { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        if (Id != Guid.Empty)
        {
            _model = await RoutineService.Get(Id) ?? new();
        }
        
        _equipments = await EquipmentService.Get();
        _exercises = await ExerciseService.Get();

        _model.RoutineExercises = await RoutineExerciseService.Get(x => x.Routine.Id == _model.Id);

        await base.OnInitializedAsync();
    }
    
    private async Task Submit(RoutineEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            _model = await RoutineService.Add(entity);
        }
        else
        {
            await RoutineService.Update(entity);
        }
        foreach (var exercise in _model.RoutineExercises)
        {
            exercise.Order = _model.RoutineExercises.IndexOf(exercise);
            exercise.Routine = _model;
            if (exercise.Id == Guid.Empty)
            {
                await RoutineExerciseService.Add(exercise);    
            }
            else
            {
                await RoutineExerciseService.Update(exercise);    
                
            }   
        }
    }

    private void Close()
    {
        DialogService.Close();
    }

    private void AddItem(MouseEventArgs arg)
    {
        _model.RoutineExercises.Add(new ());
        StateHasChanged();
    }

    private void Remove(RoutineExerciseEntity entity)
    {
        _model.RoutineExercises.Remove(entity);
    }
}
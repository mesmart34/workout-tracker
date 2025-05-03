using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class RoutineEditor : ComponentBase
{
    private RoutineEntity? _model = new();
    private List<EquipmentEntity> _equipments = new();
    private List<ExerciseEntity> _exercises = new();
    private List<RoutineExerciseEntity> _routineExerciseEntities = new();

    [Parameter]
    public Guid Id { get; set; }
    
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
            _model = await RoutineService.Get(Id);
        }
        
        _equipments = await EquipmentService.Get();
        _exercises = await ExerciseService.Get();

        await base.OnInitializedAsync();
    }
    
    private async Task Submit(RoutineEntity entity)
    {
        var routine = await RoutineService.Add(entity);
        foreach (var exercise in _routineExerciseEntities)
        {
            exercise.Order = _routineExerciseEntities.IndexOf(exercise);
            exercise.Routine = routine;
            exercise.Id = Guid.NewGuid();
        }

        await RoutineExerciseService.AddRange(_routineExerciseEntities);
    }

    private void Close()
    {
        NavigationManager.NavigateTo("routines/");
    }

    private void AddItem(MouseEventArgs arg)
    {
        _routineExerciseEntities.Add(new ());
        StateHasChanged();
    }
}
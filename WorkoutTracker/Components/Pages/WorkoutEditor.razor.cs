using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Components.Shared;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutEditor : ComponentBase
{
    private List<RoutineEntity> _routines = new();
    private WorkoutSessionEntity _model = new();
    private RadzenSteps _steps = new();
    private Dictionary<WorkoutSessionExerciseEntity, WorkoutExerciseEditor> _workoutExerciseEditors = new();
    
    [Parameter]
    public Guid? Id { get; set; }
    
    [Inject] 
    private WorkoutSessionService WorkoutSessionService { get; set; } = null!;
    
    [Inject] 
    private DialogService DialogService { get; set; } = null!;
    
    [Inject] 
    public RoutineService RoutineService { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        _routines = await RoutineService.Get();
        if (Id.HasValue && Id != Guid.Empty)
        {
            _model = await WorkoutSessionService.Get(Id.Value) ?? new(); 
        }

        foreach (var workoutSessionExerciseEntity in _model.Exercises)
        {
            _workoutExerciseEditors.Add(workoutSessionExerciseEntity, new WorkoutExerciseEditor());
        }
        
        await base.OnInitializedAsync();
    }

    private void LoadModel()
    {
        if (_model.Exercises.Count != 0 || _model.Routine?.RoutineExercises == null)
        {
            return;
        }
        
        _model.Duration = TimeSpan.FromHours(1);
        _model.WorkoutDate = DateTime.UtcNow;
        foreach (var routineExercise in _model.Routine.RoutineExercises)
        {
            if (routineExercise.Exercise == null)
            {
                continue;
            }

            _model.Exercises.Add(new WorkoutSessionExerciseEntity()
            {
                Exercise = routineExercise.Exercise,
                Order = routineExercise.Order,
                WorkoutSession = _model,
                Sets = new()
                {
                    new SetEntity()
                    {
                        Order = 0
                    }
                }
            });
        }
    }

    private void StepChanged()
    {
        if (!_model.Complete)
        {
            LoadModel();
        }
    }

    private async Task Complete(MouseEventArgs arg)
    {
        _model.Complete = true;

        if (_model.Id == Guid.Empty)
        {
            _model = await WorkoutSessionService.Add(_model);
        }
        else
        {
            await WorkoutSessionService.Update(_model);
        }
        
        foreach (var workoutExerciseEditor in _workoutExerciseEditors)
        {
            workoutExerciseEditor.Value?.Save();
        }
        
    }
}
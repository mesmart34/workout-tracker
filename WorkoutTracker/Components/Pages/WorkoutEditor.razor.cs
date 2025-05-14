using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutEditor : ComponentBase
{
    private WorkoutSessionEntity _model = new();
    private List<RoutineEntity> _routines = new();
    private RadzenSteps _steps = new();
    private bool _canChange;
    private bool _trackMode = false;
    private int _stepIndex = 0;
    private IEnumerable<Enum>? _moods;
    private RoutineExerciseEntity? _currentExercise = null;
    private bool _showButtons = true;

    [Parameter]
    public Guid? Id { get; set; }

    [Inject] 
    private WorkoutSessionService WorkoutSessionService { get; set; } = null!;

    [Inject] 
    public RoutineService RoutineService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _moods = Enum.GetValues<Mood>().Cast<Enum>();
        if (Id != null)
        {
            _model = await WorkoutSessionService.Get(Id.Value) ?? new();
            _stepIndex = 1;
        }
        else
        {
            _model.WorkoutDate = DateTime.Now;
            _model.Mood = Mood.Ok;
            
        }
        _routines = await RoutineService.Get();
        await base.OnInitializedAsync();
    }

    private void RoutineChanged(object value)
    {
        _canChange = true;
    }
    
    private void OnStepChange(int step)
    {
        
    }

    private void StepChange(StepsCanChangeEventArgs arg)
    {
        if (!_canChange)
        {
            arg.PreventDefault();
        }
    }

    private void ChangeMode(RoutineExerciseEntity routineExercise)
    {
        _trackMode = true;
        _showButtons = false;
        _currentExercise = routineExercise;
    }

    private void AddSet()
    {
        // if (_selectedExercise?.Exercise != null)
        // {
        //     _selectedWorkout?.Sets.Add(new()
        //     {
        //         Exercise = _selectedExercise.Exercise,
        //         ExerciseId = _selectedExercise.ExerciseId.Value
        //     });
        // }
    }

    private void RemoveLastSet()
    {
        // _selectedWorkout?.Sets.RemoveAt(_selectedWorkout.Sets.Count - 1);
    }

    private void Back()
    {
        _trackMode = false;
        _showButtons = true;
        _currentExercise = null;
    }
}
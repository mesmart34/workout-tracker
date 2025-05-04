using Microsoft.AspNetCore.Components;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutEditor : ComponentBase
{
    private WorkoutSessionEntity? _model;
    private Dictionary<ExerciseEntity, List<SetEntity>>? _workout = new();
    
    [Parameter]
    public Guid? Id { get; set; }

    [Inject] 
    private WorkoutSessionService WorkoutSessionService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Id != null)
        {
            _model = await WorkoutSessionService.Get(Id.Value);
        }
        
        _workout = _model?.Sets.GroupBy(x => x.Exercise).ToDictionary(x => x.Key, x => x.ToList());
        
        await base.OnInitializedAsync();
    }
}
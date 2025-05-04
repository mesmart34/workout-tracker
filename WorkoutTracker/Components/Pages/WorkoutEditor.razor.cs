using Microsoft.AspNetCore.Components;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutEditor : ComponentBase
{
    private WorkoutSessionEntity _model = new();
    
    [Parameter]
    public Guid? Id { get; set; }

    [Inject] 
    private WorkoutSessionService WorkoutSessionService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Id != null)
        {
            _model = await WorkoutSessionService.Get(Id.Value) ?? new();
        }
        
        await base.OnInitializedAsync();
    }
}
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Common;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutSchedule : ComponentBase
{
    private RadzenScheduler<WorkoutSessionAppointment> _scheduler = new();
    private List<WorkoutSessionAppointment> _appointments = new();
    
    [Inject] 
    private WorkoutSessionService WorkoutSessionService { get; set; } = null!;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _appointments = (await WorkoutSessionService.Get()).Select(x => new WorkoutSessionAppointment()
        {
            Start = x.WorkoutDate,
            End = x.WorkoutDate.Add(x.Duration ?? TimeSpan.FromHours(1)),
            WorkoutName = x.Routine?.Name!,
            WorkoutSessionId = x.Id
        }).ToList();
        await base.OnInitializedAsync();
    }

    private void OnSlotSelect(SchedulerAppointmentSelectEventArgs<WorkoutSessionAppointment> schedulerAppointmentSelectEventArgs)
    {
        var id = schedulerAppointmentSelectEventArgs.Data.WorkoutSessionId;
        NavigationManager.NavigateTo($"/workouts/editor/{id}");
    }
}
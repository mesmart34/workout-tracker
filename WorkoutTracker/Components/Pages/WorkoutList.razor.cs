using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Common;
using WorkoutTracker.Components.Shared;
using WorkoutTracker.Components.Shared.Dialogs;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class WorkoutList : ComponentBase
{
    private DataGrid<WorkoutSessionEntity> _grid = new();

    private List<ColumnDescription<WorkoutSessionEntity>> _columns = new()
    {
        new ColumnDescription<WorkoutSessionEntity>()
        {
            Caption = nameof(WorkoutSessionEntity.Routine.Name),
            Template = x => ((WorkoutSessionEntity)x).Routine?.Name
        },
        new ColumnDescription<WorkoutSessionEntity>()
        {
            Caption = nameof(WorkoutSessionEntity.WorkoutDate),
            Template = x => ((WorkoutSessionEntity)x).WorkoutDate.ToString("dd/MM/yyyy")
        }
    };

    [Inject] 
    public WorkoutSessionService WorkoutSessionService { get; set; } = null!;

    [Inject] 
    public DialogService DialogService { get; set; } = null!;

    [Inject] 
    public NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private void AddItem()
    {
        NavigationManager.NavigateTo("/workouts/editor");
    }

    private void DeleteItem(WorkoutSessionEntity item)
    {
        
    }

    private void EditItem(WorkoutSessionEntity item)
    {
        NavigationManager.NavigateTo($"/workouts/editor/{item.Id}");
    }
}
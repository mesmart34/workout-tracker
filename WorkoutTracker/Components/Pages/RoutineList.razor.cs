using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Common;
using WorkoutTracker.Components.Shared;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class RoutineList : ComponentBase
{
    private DataGrid<RoutineEntity> _grid = new();

    private List<ColumnDescription<RoutineEntity>> _columns = new()
    {
        new ColumnDescription<RoutineEntity>()
        {
            Name = nameof(RoutineEntity.Name),
            Caption = "Name"
        }
    };

    [Inject] public RoutineService RoutineService { get; set; } = null!;

    [Inject] public DialogService DialogService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async void AddItem()
    {
        var item = new RoutineEntity();
        var result = await DialogService.OpenAsync<EquipmentEdit>("Routine Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await RoutineService.Add(item);
            await _grid.Reload();
        }
    }

    private async void DeleteItem(RoutineEntity item)
    {
        await RoutineService.Remove(item);
        await _grid.Reload();
    }

    private async void EditItem(RoutineEntity item)
    {
        var result = await DialogService.OpenAsync<EquipmentEdit>("Routine Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await RoutineService.Update(item);
            await _grid.Reload();
        }
    }
}
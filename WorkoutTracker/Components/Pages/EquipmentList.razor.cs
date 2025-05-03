using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Common;
using WorkoutTracker.Components.Shared;
using WorkoutTracker.Components.Shared.Dialogs;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Pages;

public partial class EquipmentList : ComponentBase
{
    private List<EquipmentEntity> _equipments = new();
    private DataGrid<EquipmentEntity> _grid = new();

    private List<ColumnDescription<EquipmentEntity>> _columns = new()
    {
        new ColumnDescription<EquipmentEntity>()
        {
            Name = nameof(EquipmentEntity.Name),
            Caption = "Name",
            Template = x => (x as  EquipmentEntity)?.Name
        }
    };

    [Inject] public EquipmentService EquipmentService { get; set; } = null!;

    [Inject] public DialogService DialogService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _equipments = await EquipmentService.Get();
        await base.OnInitializedAsync();
    }

    private async void AddItem()
    {
        var item = new EquipmentEntity();
        var result = await DialogService.OpenAsync<EquipmentDialog>("Equipment Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await EquipmentService.Add(item);
            await _grid.Reload();
        }
    }

    private async void DeleteItem(EquipmentEntity item)
    {
        await EquipmentService.Remove(item);
        await _grid.Reload();
    }

    private async void EditItem(EquipmentEntity item)
    {
        var result = await DialogService.OpenAsync<EquipmentDialog>("Equipment Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await EquipmentService.Update(item);
            await _grid.Reload();
        }
    }
}
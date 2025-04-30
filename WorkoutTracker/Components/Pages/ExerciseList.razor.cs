using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Application.Utils;
using WorkoutTracker.Common;
using WorkoutTracker.Components.Shared;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Components.Pages;

public partial class ExerciseList : ComponentBase
{
    private DataGrid<ExerciseEntity> _grid = new();

    private List<ColumnDescription<ExerciseEntity>> _columns = new()
    {
        new ColumnDescription<ExerciseEntity>()
        {
            Name = nameof(ExerciseEntity.Name),
            Caption = Resource.Name,
            Template = x => ((ExerciseEntity)x).Name
        },
        new ColumnDescription<ExerciseEntity>()
        {
            Name = nameof(ExerciseEntity.ExerciseType),
            Caption = Resource.Type,
            Template = x => ((ExerciseEntity)x)?.ExerciseType?.GetEnumDescription()
        }
    };

    [Inject] 
    public ExerciseService ExerciseService { get; set; } = null!;

    [Inject] 
    public DialogService DialogService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async void AddItem()
    {
        var item = new ExerciseEntity();
        var result = await DialogService.OpenAsync<ExerciseEditor>("Exercise Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await ExerciseService.Add(item);
            await _grid.Reload();
        }
    }

    private async void DeleteItem(ExerciseEntity item)
    {
        await ExerciseService.Remove(item);
        await _grid.Reload();
    }

    private async void EditItem(ExerciseEntity item)
    {
        var result = await DialogService.OpenAsync<ExerciseEditor>("Exercise Editor", new Dictionary<string, object>()
        {
            ["Model"] = item
        });
        if (result == true)
        {
            await ExerciseService.Update(item);
            await _grid.Reload();
        }
    }
}
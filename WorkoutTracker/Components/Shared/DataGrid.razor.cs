using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Common;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Components.Shared;

public partial class DataGrid<T> where T : BaseEntity, new()
{
    private List<T> _data = new();
    private RadzenDataGrid<T> _radzenDataGrid = new();

    [Inject] public DialogService DialogService { get; set; } = null!;
    
    [Parameter] 
    public List<ColumnDescription<T>> Columns { get; set; } = new();
    
    [Parameter] 
    [Required]
    public IScopedService<T> Service { get; set; } = null!;
    
    [Parameter]
    public Action? AddItemAction { get; set; }
    
    [Parameter]
    public Action<T>? EditItemAction { get; set; }
    
    [Parameter]
    public Action<T>? DeleteItemAction { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _data = await Service.Get();
        await base.OnInitializedAsync();
    }

    private async Task LoadData(LoadDataArgs args)
    {
        _data = await Service.Get();
    }

    public async Task Reload()
    {
        _data = await Service.Get();
        await _radzenDataGrid.Reload();
        StateHasChanged();
    }
}
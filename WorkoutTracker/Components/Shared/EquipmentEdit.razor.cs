using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Radzen;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared;

public partial class EquipmentEdit : ComponentBase
{
    private EquipmentEntity _model = new EquipmentEntity();
    
    [Required] [Parameter] 
    public EquipmentEntity Model { get; set; } = null!;
    
    [Inject]
    public DialogService DialogService { get; set; } = null!;

    protected override Task OnInitializedAsync()
    {
        _model.Name = Model.Name;
        return base.OnInitializedAsync();
    }

    private void Submit(EquipmentEntity item)
    {
        Model.Name = _model.Name;
        DialogService.Close(true);
    }

    private void Close()
    {
        DialogService.Close(false);
    }
}
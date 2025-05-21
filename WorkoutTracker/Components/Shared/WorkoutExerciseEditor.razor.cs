using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Components.Shared;

public partial class WorkoutExerciseEditor : ComponentBase
{
    private bool _opened;
    private List<SetEntity> _toAdd = new();
    private List<SetEntity> _toRemove = new();
    
    [Parameter]
    public WorkoutSessionExerciseEntity Model { get; set; } = null!;

    [Inject] 
    private DialogService DialogService { get; set; } = null!;
    
    [Inject] 
    private SetService SetService { get; set; } = null!;
    
    [Inject] 
    private  WorkoutSessionExerciseService  WorkoutSessionExerciseService { get; set; } = null!;

    public async Task Save()
    {
        if (Model.Id == Guid.Empty)
        {
            Model = await WorkoutSessionExerciseService.Add(Model);
        }
        else
        {
            //Model = await WorkoutSessionExerciseService.Update(Model);
        }
        
        var toUpdate = Model.Sets.Where(x => x.Id != Guid.Empty).ToList();
        //await SetService.UpdateRange(toUpdate);

        await SetService.AddRange(_toAdd);
        
        //await SetService.Remove(_toRemove);
    }
    
    private void CalculateOrder()
    {
        foreach (var set in Model.Sets)
        {
            set.Order = Model.Sets.IndexOf(set);
        }
    }
    
    private void Add(SetEntity setToInsertAfter)
    {
        var index = Model.Sets.IndexOf(setToInsertAfter);
        var set = new SetEntity()
        {
            WorkoutSessionExerciseEntity = Model
        };
        Model.Sets.Insert(index + 1, set);
        _toAdd.Add(set);
        CalculateOrder();
    }

    private void Remove(SetEntity set)
    {
        if (set.Id != Guid.Empty)
        {
            _toRemove.Add(set);
        }
        Model.Sets.Remove(set);
        CalculateOrder();
    }

    private void SwitchMode()
    {
        _opened = !_opened;
    }
}
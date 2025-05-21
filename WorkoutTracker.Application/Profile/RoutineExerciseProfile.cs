using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class RoutineExerciseProfile : AutoMapper.Profile
{
    public RoutineExerciseProfile()
    {
        CreateMap<TableRoutineExerciseEntity, RoutineExerciseEntity>();
        CreateMap<RoutineExerciseEntity, TableRoutineExerciseEntity>();
    }
}
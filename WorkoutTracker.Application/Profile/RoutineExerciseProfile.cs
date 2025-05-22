using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class RoutineExerciseProfile : AutoMapper.Profile
{
    public RoutineExerciseProfile()
    {
        CreateMap<TableRoutineExerciseEntity, RoutineExerciseEntity>();
        CreateMap<RoutineExerciseEntity, TableRoutineExerciseEntity>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Exercise, o => o.Ignore())
            .ForMember(x => x.Routine, o => o.Ignore());
    }
}
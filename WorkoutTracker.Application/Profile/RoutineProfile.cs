using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class RoutineProfile : AutoMapper.Profile
{
    public RoutineProfile()
    {
        CreateMap<TableRoutineEntity, RoutineEntity>();
        CreateMap<RoutineEntity, TableRoutineEntity>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.RoutineExercises, o => o.Ignore());
    }
}
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class ExerciseProfile : AutoMapper.Profile
{
    public ExerciseProfile()
    {
        CreateMap<TableExerciseEntity, ExerciseEntity>();
        CreateMap<ExerciseEntity, TableExerciseEntity>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Equipment, o => o.Ignore())
            .ForMember(x => x.RoutineExercises, o => o.Ignore());
    }
}
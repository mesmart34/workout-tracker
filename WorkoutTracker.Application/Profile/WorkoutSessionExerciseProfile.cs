using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class WorkoutSessionExerciseProfile : AutoMapper.Profile
{
    public WorkoutSessionExerciseProfile()
    {
        CreateMap<WorkoutSessionExerciseEntity, TableWorkoutSessionExerciseEntity>()
            .ForMember(x => x.Exercise, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.WorkoutSession, o => o.Ignore());
        
        CreateMap<TableWorkoutSessionExerciseEntity, WorkoutSessionExerciseEntity>();
    }
}
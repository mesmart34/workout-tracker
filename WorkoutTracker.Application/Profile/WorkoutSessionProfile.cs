using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class WorkoutSessionProfile : AutoMapper.Profile
{
    public WorkoutSessionProfile()
    {
        CreateMap<WorkoutSessionEntity, TableWorkoutSessionEntity>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Routine, o => o.Ignore())
            .ForMember(x => x.Exercises, o => o.Ignore());
        
        CreateMap<TableWorkoutSessionEntity, WorkoutSessionEntity>();
    }
}
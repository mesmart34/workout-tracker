using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class SetProfile : AutoMapper.Profile
{
    public SetProfile()
    {
        CreateMap<TableSetEntity, SetEntity>();
        CreateMap<SetEntity, TableSetEntity>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.WorkoutSessionExerciseEntity, o => o.Ignore());

    }
}
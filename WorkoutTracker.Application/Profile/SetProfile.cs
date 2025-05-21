using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class SetProfile : AutoMapper.Profile
{
    public SetProfile()
    {
        CreateMap<TableSetEntity, SetEntity>();
        CreateMap<SetEntity, TableSetEntity>();
    }
}
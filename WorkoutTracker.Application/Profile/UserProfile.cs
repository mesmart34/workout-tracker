using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<TableUserEntity, UserEntity>();
        CreateMap<UserEntity, TableUserEntity>();
    }
}
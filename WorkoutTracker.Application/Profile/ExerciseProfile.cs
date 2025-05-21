using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class ExerciseProfile : AutoMapper.Profile
{
    public ExerciseProfile()
    {
        CreateMap<TableExerciseEntity, ExerciseEntity>();
        CreateMap<ExerciseEntity, TableExerciseEntity>();
    }
}
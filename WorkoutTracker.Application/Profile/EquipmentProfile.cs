using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Application.Profile;

public class EquipmentProfile : AutoMapper.Profile
{
    public EquipmentProfile()
    {
        CreateMap<TableEquipmentEntity, EquipmentEntity>();
        CreateMap<EquipmentEntity, TableEquipmentEntity>();
    }
}
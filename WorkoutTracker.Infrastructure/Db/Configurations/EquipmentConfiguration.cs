using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<TableEquipmentEntity>
{
    public void Configure(EntityTypeBuilder<TableEquipmentEntity> builder)
    {
        builder.ConfigureNamed("equipment");
    }
}
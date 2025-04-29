using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<EquipmentEntity>
{
    public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
    {
        builder.ToTable("equipment");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.DateCreated)
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .IsRequired();

        builder.Property(x => x.IsDeleted);
    }
}
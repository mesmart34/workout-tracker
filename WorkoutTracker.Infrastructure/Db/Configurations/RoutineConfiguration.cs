using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class RoutineConfiguration : IEntityTypeConfiguration<RoutineEntity>
{
    public void Configure(EntityTypeBuilder<RoutineEntity> builder)
    {
        builder.ToTable("routine");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(x => x.Exercises)
            .WithOne()
            .HasForeignKey(x => x.EquipmentId);

        builder.Property(x => x.DateCreated)
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .IsRequired();

        builder.Property(x => x.IsDeleted);
    }
}
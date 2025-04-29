using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("exercise");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.EquipmentId);
        builder.HasOne(x => x.Equipment)
            .WithMany()
            .HasForeignKey(x => x.EquipmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.DateCreated)
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .IsRequired();

        builder.Property(x => x.IsDeleted);
    }
}
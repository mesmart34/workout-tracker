using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<TableExerciseEntity>
{
    public void Configure(EntityTypeBuilder<TableExerciseEntity> builder)
    {
        builder.ConfigureNamed("exercise");

        builder.Property(x => x.MuscleGroup)
            .IsRequired();
        
        builder.Property(x => x.ExerciseType)
            .IsRequired();
        
        builder.Property(x => x.EquipmentId);
        builder.HasOne(x => x.Equipment)
            .WithMany()
            .HasForeignKey(x => x.EquipmentId);

        builder.HasMany(x => x.RoutineExercises)
            .WithOne(x => x.Exercise)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
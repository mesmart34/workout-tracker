using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ConfigureNamed("exercise");

        builder.Property(x => x.MuscleGroup)
            .IsRequired();
        
        builder.Property(x => x.ExerciseType)
            .IsRequired();

        builder.Navigation(x => x.Equipment).AutoInclude();
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
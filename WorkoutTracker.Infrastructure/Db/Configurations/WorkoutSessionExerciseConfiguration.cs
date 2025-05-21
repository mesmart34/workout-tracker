using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class WorkoutSessionExerciseConfiguration : IEntityTypeConfiguration<WorkoutSessionExerciseEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutSessionExerciseEntity> builder)
    {
        builder.Configure("workout_session_exercise");

        builder.HasMany(x => x.Sets)
            .WithOne(x => x.WorkoutSessionExerciseEntity)
            .HasForeignKey(x => x.WorkoutSessionExerciseId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(x => x.Order)
            .IsRequired();
        
        builder.Property(x => x.ExerciseId).IsRequired();
        builder.HasOne(x => x.Exercise)
            .WithMany()
            .HasForeignKey(x => x.ExerciseId);
    }
}
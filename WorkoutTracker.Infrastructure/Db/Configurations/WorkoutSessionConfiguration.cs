using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class WorkoutSessionConfiguration : IEntityTypeConfiguration<TableWorkoutSessionEntity>
{
    public void Configure(EntityTypeBuilder<TableWorkoutSessionEntity> builder)
    {
        builder.Configure("workout_session");

        builder.HasMany(x => x.Exercises)
            .WithOne(x => x.WorkoutSession)
            .HasForeignKey(x => x.WorkoutSessionId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(x => x.Mood)
            .IsRequired();
        
        builder.Property(x => x.WorkoutDate)
            .IsRequired();
        
        builder.Property(x => x.Duration)
            .IsRequired();
        
        builder.Property(x => x.Notes)
            .IsRequired();

        builder.Property(x => x.Complete).IsRequired();

        builder.Property(x => x.RoutineId).IsRequired();
        builder.HasOne(x => x.Routine)
            .WithMany()
            .HasForeignKey(x => x.RoutineId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSessionEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutSessionEntity> builder)
    {
        builder.Configure("workout_session");
        
        builder.HasMany(x => x.Sets)
            .WithOne(x => x.WorkoutSession)
            .HasForeignKey(x => x.WorkoutSessionId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(x => x.Mood)
            .IsRequired();
    }
}
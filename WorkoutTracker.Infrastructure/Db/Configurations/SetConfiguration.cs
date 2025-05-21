using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class SetConfiguration : IEntityTypeConfiguration<SetEntity>
{
    public void Configure(EntityTypeBuilder<SetEntity> builder)
    {
        builder.Configure("set");
        
        builder.HasOne(x => x.WorkoutSessionExerciseEntity)
            .WithMany(x => x.Sets)
            .HasForeignKey(x => x.WorkoutSessionExerciseId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.Duration);
        
        builder.Property(x => x.Order).IsRequired();

        builder.Property(x => x.Reps)
            .IsRequired();
        
        builder.Property(x => x.Weight)
            .IsRequired();
    }
}
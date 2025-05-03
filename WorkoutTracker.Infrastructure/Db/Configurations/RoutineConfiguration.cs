using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class RoutineConfiguration : IEntityTypeConfiguration<RoutineEntity>
{
    public void Configure(EntityTypeBuilder<RoutineEntity> builder)
    {
        builder.ConfigureNamed("routine");

        builder.HasMany(x => x.RoutineExercises)
            .WithOne(x => x.Routine)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
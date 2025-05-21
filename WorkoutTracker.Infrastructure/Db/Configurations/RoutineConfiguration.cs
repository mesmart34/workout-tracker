using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class RoutineConfiguration : IEntityTypeConfiguration<TableRoutineEntity>
{
    public void Configure(EntityTypeBuilder<TableRoutineEntity> builder)
    {
        builder.ConfigureNamed("routine");
        
        builder.HasMany(x => x.RoutineExercises)
            .WithOne(x => x.Routine)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
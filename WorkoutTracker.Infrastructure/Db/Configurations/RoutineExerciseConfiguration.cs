using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class RoutineExerciseConfiguration : IEntityTypeConfiguration<RoutineExerciseEntity>
{
    public void Configure(EntityTypeBuilder<RoutineExerciseEntity> builder)
    {
        builder.Configure("routine_exercise");
        
        builder.Property(x => x.Order);
        
        builder.Property(x => x.RoutineId)
            .IsRequired();
        
        builder.Navigation(x => x.Routine).AutoInclude();
        builder.HasOne(x => x.Routine)
            .WithMany(x => x.RoutineExercises)
            .HasForeignKey(x => x.RoutineId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Navigation(x => x.Exercise).AutoInclude();
        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.RoutineExercises)
            .HasForeignKey(x => x.ExerciseId);
    }
}
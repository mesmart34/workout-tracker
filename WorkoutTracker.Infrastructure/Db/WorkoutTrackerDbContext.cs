using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db.Configurations;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db;

public sealed class WorkoutTrackerDbContext : DbContext
{
    public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> contextOptions) : base(contextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EquipmentConfiguration).Assembly
        );
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TableUserEntity> Users { get; set; }
    
    public DbSet<TableEquipmentEntity> Equipments { get; set; }
    
    public DbSet<TableExerciseEntity> Exercises { get; set; }
    
    public DbSet<TableRoutineEntity> Routines { get; set; }
    
    public DbSet<TableRoutineExerciseEntity> RoutineExercises { get; set; }
    
    public DbSet<TableWorkoutSessionEntity> WorkoutSessions { get; set; }
    
    public DbSet<TableWorkoutSessionExerciseEntity> WorkoutSessionsExercises { get; set; }
    
    public DbSet<TableSetEntity> Sets { get; set; }
    
}
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db.Configurations;

namespace WorkoutTracker.Infrastructure.Db;

public sealed class WorkoutTrackerDbContext : DbContext
{
    public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> contextOptions) : base(contextOptions)
    {
        //Database.EnsureCreated();
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

    public DbSet<EquipmentEntity> Equipments { get; set; }
    
    public DbSet<ExerciseEntity> Exercises { get; set; }
    
    public DbSet<RoutineEntity> Routines { get; set; }
    
    public DbSet<RoutineExerciseEntity> RoutineExercises { get; set; }
    
    public DbSet<WorkoutSessionEntity> WorkoutSessions { get; set; }
    
    public DbSet<SetEntity> Sets { get; set; }
    
}
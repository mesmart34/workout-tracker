using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker;

public static class ConfigureDb
{
    public static async Task AddDb(this IHost host)
    {
        await using var scope = host.Services.CreateAsyncScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<WorkoutTrackerDbContext>>();
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        //await dbContext.SeedData();
    }

    private static async Task SeedData(this WorkoutTrackerDbContext db)
    {
        
        if (!db.Equipments.Any())
        {
            return;
        }
        var weightedVest = await db.Equipments.AddAsync(new EquipmentEntity()
        {
            Name = "Weighted Vest"
        });

        var exercise = await db.Exercises.AddAsync(new ExerciseEntity()
        {
            Name = "Weighted pull up",
            EquipmentId =  weightedVest.Entity.Id,
        });

        await db.SaveChangesAsync();
    }
}
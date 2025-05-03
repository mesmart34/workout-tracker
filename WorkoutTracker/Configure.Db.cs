using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Common;
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

        await dbContext.SeedData();
    }

    private static async Task SeedData(this WorkoutTrackerDbContext db)
    {
        if (!db.Equipments.Any())
        {
            await db.Equipments.AddRangeAsync(
                new EquipmentEntity()
                {
                    Name = "Equipment 1"
                },
                new EquipmentEntity()
                {
                    Name = "Equipment 2"
                });
            await db.SaveChangesAsync();
        }

        if (!db.Exercises.Any())
        {
            await db.Exercises.AddRangeAsync(new ExerciseEntity()
                {
                    Name = "Pull ups",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine
                });
            await db.SaveChangesAsync();
        }

        if (!db.Routines.Any())
        {
            await db.Routines.AddRangeAsync(new RoutineEntity()
                {
                    Name = "Routine 1"
                });
             await db.SaveChangesAsync();
        }

        if (!db.RoutineExercises.Any())
        {
            await db.RoutineExercises.AddRangeAsync(new RoutineExerciseEntity()
                {
                    Exercise = db.Exercises.First(),
                    Routine = db.Routines.First(),
                    Notes = "lol kek",
                    Order = 0
                });
            await db.SaveChangesAsync();
        }

        if (!db.WorkoutSessions.Any())
        {
            await db.WorkoutSessions.AddRangeAsync(new WorkoutSessionEntity()
            {
                Mood = Mood.Ok,
                Duration = TimeSpan.FromHours(1),
                WorkoutDate =  DateTime.UtcNow.AddHours(-5)
            });
            await db.SaveChangesAsync();
        }

        if (!db.Sets.Any())
        {
            await db.Sets.AddRangeAsync(new SetEntity()
            {
                Exercise =  db.Exercises.First(),
                Weight = 25,
                Duration =  TimeSpan.FromMinutes(5),
                Reps = 5,
                WorkoutSession = db.WorkoutSessions.First()
            });
            await db.SaveChangesAsync();
        }

        await db.SaveChangesAsync();
    }
}
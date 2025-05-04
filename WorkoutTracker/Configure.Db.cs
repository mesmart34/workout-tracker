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
                    Name = "Highlets",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                },
                new EquipmentEntity()
                {
                    Name = "Parallets",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                },
                new EquipmentEntity()
                {
                    Name = "Weighted vest",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
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
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                },
                new ExerciseEntity()
                {
                    Name = "Chin ups",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                },
                new ExerciseEntity()
                {
                    Name = "Rows",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });
            await db.SaveChangesAsync();
        }

        if (!db.Routines.Any())
        {
            await db.Routines.AddRangeAsync(new RoutineEntity()
            {
                Name = "Pull day",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }

        if (!db.RoutineExercises.Any())
        {
            var exercises = await db.Exercises.ToListAsync();
            var routineExercises = db.Exercises.ToList().Select(x => new RoutineExerciseEntity()
            {
                Exercise = x,
                Notes = string.Empty,
                Order = exercises.IndexOf(x),
                Routine = db.Routines.FirstAsync().Result,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            });
            await db.RoutineExercises.AddRangeAsync(routineExercises);
            await db.SaveChangesAsync();
        }

        if (!db.WorkoutSessions.Any())
        {
            var routine = await db.Routines.FirstAsync();
            await db.WorkoutSessions.AddRangeAsync(new WorkoutSessionEntity()
            {
                Mood = Mood.Ok,
                Duration = TimeSpan.FromHours(1),
                WorkoutDate = DateTime.UtcNow.AddHours(-5),
                Routine = routine,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            });
            await db.SaveChangesAsync();
        }

        if (!db.Sets.Any())
        {
            var workout = await db.WorkoutSessions.FirstAsync();
            var exercises = workout.Routine.RoutineExercises
                .Select(x => x.Exercise)
                .ToList();
            var sets = exercises
                .Where(x => x != null)
                .SelectMany(x =>
                new[]
                {
                    new SetEntity()
                    {
                        Duration = TimeSpan.FromMinutes(5),
                        Reps = 14,
                        Weight = 25,
                        WorkoutSession = workout,
                        Exercise = x!,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    },
                    new SetEntity()
                    {
                        Duration = TimeSpan.FromMinutes(5),
                        Reps = 9,
                        Weight = 25,
                        WorkoutSession = workout,
                        Exercise = x!,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    }
                }
            ).ToList();
            await db.Sets.AddRangeAsync(sets);
            await db.SaveChangesAsync();
        }
    }
}
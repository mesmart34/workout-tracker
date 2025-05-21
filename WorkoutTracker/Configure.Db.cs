using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Domain.Common;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Db;
using WorkoutTracker.Infrastructure.Entities;

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
        if (!db.Users.Any())
        {
            await db.Users.AddAsync(new TableUserEntity()
            {
                FirstName = "John",
                LastName = "Doe",
                IsAdmin = true,
                Email = "johndoe@gmail.com",
                Password = "1234"
            });
            
            await db.SaveChangesAsync();
        }
        
        var user = await db.Users.FirstAsync();
        
        if (!db.Equipments.Any())
        {
            await db.Equipments.AddRangeAsync(
                new TableEquipmentEntity()
                {
                    Name = "Highlets",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                },
                new TableEquipmentEntity()
                {
                    Name = "Parallets",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                },
                new TableEquipmentEntity()
                {
                    Name = "Weighted vest",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                });
            await db.SaveChangesAsync();
        }

        if (!db.Exercises.Any())
        {
            await db.Exercises.AddRangeAsync(new TableExerciseEntity()
                {
                    Name = "Pull ups",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                },
                new TableExerciseEntity()
                {
                    Name = "Chin ups",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                },
                new TableExerciseEntity()
                {
                    Name = "Rows",
                    Equipment = db.Equipments.First(),
                    ExerciseType = ExerciseType.Weight,
                    MuscleGroup = MuscleGroup.Spine,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                });
            await db.SaveChangesAsync();
        }

        if (!db.Routines.Any())
        {
            await db.Routines.AddRangeAsync(new TableRoutineEntity()
            {
                Name = "Pull day",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                User = user,
            });
            await db.SaveChangesAsync();
        }

        if (!db.RoutineExercises.Any())
        {
            var exercises = await db.Exercises.ToListAsync();
            var routineExercises = db.Exercises.ToList().Select(x => new TableRoutineExerciseEntity()
            {
                Exercise = x,
                Notes = string.Empty,
                Order = exercises.IndexOf(x),
                Routine = db.Routines.FirstAsync().Result,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                User = user,
            });
            await db.RoutineExercises.AddRangeAsync(routineExercises);
            await db.SaveChangesAsync();
        }

        if (!db.WorkoutSessions.Any())
        {
            var routine = await db.Routines.FirstAsync();
            await db.WorkoutSessions.AddRangeAsync(new TableWorkoutSessionEntity()
            {
                Mood = Mood.Ok,
                Duration = TimeSpan.FromHours(1),
                WorkoutDate = DateTime.UtcNow.AddHours(-5),
                Routine = routine,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                User = user,
            });
            await db.SaveChangesAsync();
        }

        if (!db.WorkoutSessionsExercises.Any())
        {
            var routine = await db.Routines.FirstAsync();
            var routineExerciseEntities = routine.RoutineExercises.ToList();
            var exercises = routineExerciseEntities
                .Select(x => x.Exercise)
                .ToList();
            var workoutSession = await db.WorkoutSessions.FirstOrDefaultAsync();
            await db.WorkoutSessionsExercises.AddRangeAsync(new []
            {
                new TableWorkoutSessionExerciseEntity()
                {
                    Exercise = exercises.First()!,
                    WorkoutSession = workoutSession!,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user,
                    Order = 0,
                    Sets = new()
                }
            });
            await db.SaveChangesAsync();
        }

        if (!db.Sets.Any())
        {
            var workoutSessionExercise = await db.WorkoutSessionsExercises.FirstAsync();

            await db.Sets.AddRangeAsync(new[]
            {
                new TableSetEntity()
                {
                    Duration = TimeSpan.FromMinutes(5),
                    Reps = 14,
                    Weight = 25,
                    WorkoutSessionExerciseEntity = workoutSessionExercise!,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user
                },
                new TableSetEntity()
                {
                    Duration = TimeSpan.FromMinutes(5),
                    Reps = 9,
                    Weight = 25,
                    WorkoutSessionExerciseEntity = workoutSessionExercise!,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    User = user
                }
            });
            await db.SaveChangesAsync();
        }
    }
}
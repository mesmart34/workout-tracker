using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Infrastructure;

public static class ConfigurationHelper
{
    public static void Configure<T>(this EntityTypeBuilder<T> builder, string tableName) where T : BaseEntity
    {
        builder.ToTable(tableName);
        
        builder.Property(x => x.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();

        builder.Property(x => x.DateCreated)
            .IsRequired();
        
        builder.Property(x => x.DateUpdated)
            .IsRequired();

        builder.Property(x => x.IsDeleted);
    }

    public static void ConfigureNamed<T>(this EntityTypeBuilder<T> builder, string tableName) where T : BaseNamedEntity
    {
        Configure(builder, tableName);
        builder.Property(x => x.Name)
            .IsRequired();
    }
}
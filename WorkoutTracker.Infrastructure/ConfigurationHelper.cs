using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Infrastructure.Contracts;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure;

public static class ConfigurationHelper
{
    public static void Configure<T>(this EntityTypeBuilder<T> builder, string tableName) where T : BaseTableEntity
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
    
    public static void ConfigureWithUser<T>(this EntityTypeBuilder<T> builder, string tableName) where T : BaseTableEntity, IHasTableUser
    {
        Configure(builder, tableName);

        builder.Navigation(x => x.User).AutoInclude();
        builder.Property(x => x.UserId);
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }

    public static void ConfigureNamed<T>(this EntityTypeBuilder<T> builder, string tableName) where T : BaseTableNamedEntity
    {
        Configure(builder, tableName);
        builder.Property(x => x.Name)
            .IsRequired();
    }
}
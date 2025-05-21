using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Entities;

namespace WorkoutTracker.Infrastructure.Db.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<TableUserEntity>
{
    public void Configure(EntityTypeBuilder<TableUserEntity> builder)
    {
        builder.Configure("user");
        
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.IsAdmin).IsRequired();
    }
}
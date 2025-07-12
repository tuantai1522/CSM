using CSM.Core.Features.Countries;
using CSM.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.NickName).HasMaxLength(128);
        builder.Property(p => p.NickName).IsRequired();
        
        builder.Property(p => p.FirstName).HasMaxLength(64);
        builder.Property(p => p.FirstName).IsRequired();

        builder.Property(p => p.MiddleName).HasMaxLength(64);
        builder.Property(p => p.LastName).HasMaxLength(64);
        
        builder.Property(p => p.Email).HasMaxLength(64);
        builder.Property(p => p.Email).IsRequired();

        builder.Property(p => p.HashPassword).HasMaxLength(256);
        builder.Property(p => p.HashPassword).IsRequired();

        // GenderType is an enum (to store string in database)
        builder.Property(p => p.GenderType)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<GenderType>(v));
        
        // One user belongs to one city
        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(u => u.CityId);
        
        builder.Property(p => p.TimeZone).HasMaxLength(64);
        builder.Property(p => p.TimeZone).IsRequired();

        builder.Property(p => p.Locale).HasMaxLength(64);
        builder.Property(p => p.Locale).IsRequired();
        
        // One user belongs to multiple channels
        builder.HasMany(r => r.ChannelMembers)
            .WithOne(x => x.User)
            .HasForeignKey(p => p.UserId);

    }
}
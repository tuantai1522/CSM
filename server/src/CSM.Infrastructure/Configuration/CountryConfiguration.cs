using CSM.Core.Features.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(64);

        // One country has multiple cities
        builder.HasMany(r => r.Cities)
            .WithOne()
            .HasForeignKey(p => p.CountryId);
    }
}
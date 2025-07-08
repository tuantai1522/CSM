using CSM.Core.Features.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Rename to snake case
        builder.ToTable("cities");
        
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(128);
    }
}
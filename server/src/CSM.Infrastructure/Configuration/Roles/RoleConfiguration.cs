using CSM.Core.Features.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Name).IsRequired();
        
        builder.Property(p => p.Description).HasMaxLength(256);

        // One role has multiple users
        builder.HasMany(r => r.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(p => p.RoleId);

    }
}
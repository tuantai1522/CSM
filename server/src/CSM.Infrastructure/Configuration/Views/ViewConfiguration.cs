using CSM.Core.Features.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration.Views;

public class ViewConfiguration : IEntityTypeConfiguration<View>
{
    public void Configure(EntityTypeBuilder<View> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(128);
        builder.Property(p => p.Name).IsRequired();
        
        builder.Property(p => p.Url).HasMaxLength(256);

        // One view has multiple children views
        builder.HasOne<View>()
            .WithMany(v => v.Views)
            .HasForeignKey(u => u.ParentViewId);
        
        // One view has multiple user permissions
        builder.HasMany(r => r.UserPermissions)
            .WithOne()
            .HasForeignKey(p => p.ViewId);
        
        // One view has multiple role permissions
        builder.HasMany(r => r.RolePermissions)
            .WithOne()
            .HasForeignKey(p => p.ViewId);

    }
}
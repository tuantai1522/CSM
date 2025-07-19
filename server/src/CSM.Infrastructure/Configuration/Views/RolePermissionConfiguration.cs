using CSM.Core.Features.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration.Views;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        // To define composite key for role_permissions table
        builder.HasKey(cm => new { cm.RoleId, cm.ViewId });
    }
}
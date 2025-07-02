using CSM.Core.Features.Channels;
using CSM.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class ChannelMemberConfiguration : IEntityTypeConfiguration<ChannelMember>
{
    public void Configure(EntityTypeBuilder<ChannelMember> builder)
    {
        // To define composite key for channel member table
        builder.HasKey(cm => new { cm.ChannelId, cm.UserId });
    }
}
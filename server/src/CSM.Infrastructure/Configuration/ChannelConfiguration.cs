using CSM.Core.Features.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.Property(x => x.DisplayName).IsRequired();
        
        // ChannelType is an enum (to store string in database)
        builder.Property(p => p.Type)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<ChannelType>(v));
        
        builder.Property(p => p.DisplayName).HasMaxLength(256);
        builder.Property(p => p.DisplayName).HasMaxLength(1024);

        // One channel is created by only one user
        builder.HasOne(c => c.Creator)
            .WithMany()
            .HasForeignKey(c => c.CreatorId);
        
        // One channel has multiple channel members
        builder.HasMany(r => r.ChannelMembers)
            .WithOne()
            .HasForeignKey(p => p.ChannelId);
        
        // One channel has multiple posts
        builder.HasMany(r => r.Posts)
            .WithOne()
            .HasForeignKey(p => p.ChannelId);

    }
}
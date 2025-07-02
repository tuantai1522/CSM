using CSM.Core.Features.Channels;
using CSM.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // One post has multiple posts in thread
        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(u => u.RootId);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(u => u.UserId);
        
        // PostType is an enum (to store string in database)
        builder.Property(p => p.Type)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PostType>(v));
    }
}
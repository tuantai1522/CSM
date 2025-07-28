using CSM.Core.Features.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration.Channels;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(p => p.Message).IsRequired();

        // One post has multiple posts in thread
        builder.HasOne<Post>()
            .WithMany()
            .HasForeignKey(u => u.RootId);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(u => u.UserId);
        
        // PostType is an enum (to store string in database)
        builder.Property(p => p.Type)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PostType>(v));
        
        // To create idx on column CreatedAt and Id
        builder
            .HasIndex(p => new { p.CreatedAt, p.Id });
    }
}
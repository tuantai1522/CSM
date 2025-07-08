using CSM.Core.Features.ErrorMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSM.Infrastructure.Configuration;

public class ErrorMessageConfiguration : IEntityTypeConfiguration<ErrorMessage>
{
    public void Configure(EntityTypeBuilder<ErrorMessage> builder)
    {
        builder.Property(p => p.Details).IsRequired();
        builder.Property(p => p.Details).HasMaxLength(512);
    }
}
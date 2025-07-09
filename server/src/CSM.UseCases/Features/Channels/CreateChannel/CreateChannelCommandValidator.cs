using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.CreateChannel;

internal sealed class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
{
    public CreateChannelCommandValidator()
    {
        RuleFor(c => c.DisplayName)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelNameEmpty.ToString());
        
        RuleFor(c => new { c.OwnerIds, c.MemberIds })
            .Must(x => (x.OwnerIds?.Count ?? 0) + (x.MemberIds?.Count ?? 0) > 0)
            .WithErrorCode(ErrorCode.ListMembersAndOwnersEmpty.ToString());
        
    }
}

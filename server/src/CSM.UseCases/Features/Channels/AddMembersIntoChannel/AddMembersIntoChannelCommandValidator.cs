using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.AddMembersIntoChannel;

internal sealed class AddMembersIntoChannelCommandValidator : AbstractValidator<AddMembersIntoChannelCommand>
{
    public AddMembersIntoChannelCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
        
        RuleFor(c => new { c.OwnerIds, c.MemberIds })
            .Must(x => (x.OwnerIds?.Count ?? 0) + (x.MemberIds?.Count ?? 0) > 0)
            .WithErrorCode(ErrorCode.ListMembersAndOwnersEmpty.ToString());
    }
}

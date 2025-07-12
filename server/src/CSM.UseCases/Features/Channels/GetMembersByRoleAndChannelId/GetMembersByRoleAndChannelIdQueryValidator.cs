using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.GetMembersByRoleAndChannelId;

internal sealed class GetMembersByRoleAndChannelIdQueryValidator : AbstractValidator<GetMembersByRoleAndChannelIdQuery>
{
    public GetMembersByRoleAndChannelIdQueryValidator()
    {
        RuleFor(c => c.ChannelId)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
        
        // A positive number
        RuleFor(c => c.Page)
            .Must(x => x > 0).WithErrorCode(ErrorCode.PageMustBePositive.ToString());

        // A positive number
        RuleFor(c => c.PageSize)
            .Must(x => x > 0).WithErrorCode(ErrorCode.PageSizeMustBePositive.ToString());
    }
}

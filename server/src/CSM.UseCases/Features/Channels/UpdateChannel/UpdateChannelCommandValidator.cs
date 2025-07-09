using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.UpdateChannel;

internal sealed class UpdateChannelCommandValidator : AbstractValidator<UpdateChannelCommand>
{
    public UpdateChannelCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
        
        RuleFor(c => c.DisplayName)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelNameEmpty.ToString());
    }
}

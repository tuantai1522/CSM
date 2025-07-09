using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.CreatePost;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.ChannelId)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
        
        RuleFor(c => c.Message)
            .NotEmpty().WithErrorCode(ErrorCode.MessageEmpty.ToString());
    }
}

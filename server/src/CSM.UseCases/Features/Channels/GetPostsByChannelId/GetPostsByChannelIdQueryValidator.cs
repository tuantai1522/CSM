using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.GetPostsByChannelId;

internal sealed class GetPostsByChannelIdQueryValidator : AbstractValidator<GetPostsByChannelIdQuery>
{
    public GetPostsByChannelIdQueryValidator()
    {
        RuleFor(c => c.ChannelId)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
    }
}

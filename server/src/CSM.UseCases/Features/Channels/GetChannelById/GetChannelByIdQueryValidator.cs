using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Channels.GetChannelById;

internal sealed class GetChannelByIdQueryValidator : AbstractValidator<GetChannelByIdQuery>
{
    public GetChannelByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ErrorCode.ChannelIdEmpty.ToString());
    }
}

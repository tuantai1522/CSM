using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetChannelsByUserId;

internal sealed class GetChannelsByUserIdQueryHandler(
    IChannelRepository channelRepository,
    IUserProvider userProvider): IRequestHandler<GetChannelsByUserIdQuery, Result<List<GetChannelsByUserIdResponse>>>
{
    public async Task<Result<List<GetChannelsByUserIdResponse>>> Handle(GetChannelsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var channels = await channelRepository.GetChannelsByUserIdAsync(userProvider.UserId, cancellationToken);

        var result = channels.Select(channel => new GetChannelsByUserIdResponse(channel.Id, channel.DisplayName))
            .ToList();

        return Result.Success(result);
    }
}

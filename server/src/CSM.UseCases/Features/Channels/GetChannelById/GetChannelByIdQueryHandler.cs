using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetChannelById;

internal sealed class GetChannelByIdQueryHandler(
    IChannelRepository channelRepository,
    IUserProvider userProvider): IRequestHandler<GetChannelByIdQuery, Result<GetChannelByIdResponse>>
{
    public async Task<Result<GetChannelByIdResponse>> Handle(GetChannelByIdQuery query, CancellationToken cancellationToken)
    {
        var channel = await channelRepository.GetChannelByIdAsync(
            query.Id, 
            cancellationToken, 
            // Include channel members
            x => x.ChannelMembers.Where(ch => ch.ChannelId == query.Id)
        );

        return channel is null ? 
            Result.Failure<GetChannelByIdResponse>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound)) : 
            Result.Success(new GetChannelByIdResponse(channel.Id, channel.DisplayName, channel.ChannelMembers.Count));
    }
}

using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.CreateChannel;

internal sealed class CreateChannelCommandHandler(
    IChannelRepository channelRepository,
    IUserProvider userProvider): IRequestHandler<CreateChannelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateChannelCommand command, CancellationToken cancellationToken)
    {
        var channel = Channel.CreateChannel(ChannelType.Group, command.DisplayName, command.Purpose, userProvider.UserId);

        // Add member Ids
        AddParticipants(channel, command.MemberIds, false);
        
        // Add owner Ids and current user as owner
        AddParticipants(channel, (command.OwnerIds ?? new List<Guid>()).Concat([userProvider.UserId]).ToList(), true);

        await channelRepository.AddChannelAsync(channel, cancellationToken);
        
        await channelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(channel.Id);
    }

    private static void AddParticipants(Channel channel, IReadOnlyList<Guid>? participantIds, bool isOwner)
    {
        foreach (var participantId in participantIds ?? [])
        {
            channel.AddMember(participantId, isOwner);
        }
    }
}

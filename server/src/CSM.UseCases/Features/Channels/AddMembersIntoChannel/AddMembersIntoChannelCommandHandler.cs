using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.AddMembersIntoChannel;

internal sealed class AddMembersIntoChannelCommandHandler(IChannelRepository channelRepository, IUserProvider userProvider): IRequestHandler<AddMembersIntoChannelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddMembersIntoChannelCommand command, CancellationToken cancellationToken)
    {
        var channel = await channelRepository.GetChannelByIdAsync(
            command.Id, 
            cancellationToken, 
            
            // Include channel members
            x => x.ChannelMembers.Where(ch => ch.ChannelId == command.Id)
        );

        if (channel is null)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }

        var addedMembersResult = await AddMembers(channel, userProvider, command.MemberIds, false);
        // If there is an error when adding new members => return that error
        if (addedMembersResult.IsFailure)
        {
            return Result.Failure<Guid>(await userProvider.Error(addedMembersResult.Error.Code, addedMembersResult.Error.Type));
        }
        
        var addedOwnerResult = await AddMembers(channel, userProvider, command.OwnerIds, true);
        // If there is an error when adding new members => return that error
        if (addedOwnerResult.IsFailure)
        {
            return Result.Failure<Guid>(await userProvider.Error(addedOwnerResult.Error.Code, addedOwnerResult.Error.Type));
        }
        
        await channelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(channel.Id);
    }

    private static async Task<Result> AddMembers(Channel channel, IUserProvider userProvider, IReadOnlyList<Guid>? participantIds, bool isOwner)
    {
        foreach (var participantId in participantIds ?? [])
        {
            var addMember = channel.AddMember(userProvider.UserId, participantId, isOwner);
            if (addMember.IsFailure)
            {
                return Result.Failure<Guid>(await userProvider.Error(addMember.Error.Code, addMember.Error.Type));
            }
        }

        return Result.Success();
    }
}

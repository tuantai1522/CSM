using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.UpdateChannel;

internal sealed class UpdateChannelCommandHandler(
    IChannelRepository channelRepository,
    IUserProvider userProvider): IRequestHandler<UpdateChannelCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateChannelCommand command, CancellationToken cancellationToken)
    {
        var channel = await channelRepository.GetChannelByIdAsync(command.Id, cancellationToken);

        if (channel is null)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }

        var updatedChannel = channel.UpdateChannel(command.DisplayName, command.Purpose, userProvider.UserId);
        
        // If there is an error when updating channel => return that error
        if (updatedChannel.IsFailure)
        {
            return Result.Failure<Guid>(await userProvider.Error(updatedChannel.Error.Code, updatedChannel.Error.Type));
        }
        
        await channelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(channel.Id);
    }
}

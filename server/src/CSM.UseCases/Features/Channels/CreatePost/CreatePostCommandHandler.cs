using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Channels.CreatePost;

internal sealed class CreatePostCommandHandler(
    IChannelRepository channelRepository,
    IUserProvider userProvider): IRequestHandler<CreatePostCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var channel = await channelRepository.GetChannelByIdAsync(
            command.ChannelId, 
            cancellationToken, 
            
            // Include channel members and posts
            x => x.ChannelMembers.Where(ch => ch.ChannelId == command.ChannelId),
            x => x.Posts.Where(ch => ch.ChannelId == command.ChannelId)
        );
        
        if (channel is null)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }

        var createdPost = channel.CreatePost(userProvider.UserId, command.RootId, command.Message);
        if (createdPost.IsFailure)
        {
            return Result.Failure<Guid>(await userProvider.Error(createdPost.Error.Code, createdPost.Error.Type));
        }
        
        await channelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(createdPost.Value.Id);
    }
}

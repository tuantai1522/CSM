using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.CreatePost;

public sealed record CreatePostCommand(Guid ChannelId, Guid? RootId, string Message) : IRequest<Result<Guid>>;

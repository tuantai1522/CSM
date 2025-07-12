using CSM.Core.Features.Channels;
using CSM.UseCases.Features.Channels.GetPostsByChannelId;

namespace CSM.UseCases.Mappers.Channels;

public static class DomainToDtoMapper
{
    public static PostDto ToPostDto(this Post post) 
        => new PostDto(post.Id, post.Message, post.CreatedAt, post.User.NickName);
}
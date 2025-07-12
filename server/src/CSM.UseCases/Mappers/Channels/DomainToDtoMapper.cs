using CSM.Core.Features.Channels;
using CSM.Core.Features.Users;
using CSM.UseCases.Dtos.Channels;

namespace CSM.UseCases.Mappers.Channels;

public static class DomainToDtoMapper
{
    public static PostDto ToPostDto(this Post post) 
        => new PostDto(post.Id, post.Message, post.CreatedAt, post.User.NickName);
    
    public static UserDto ToUserDto(this User user) 
        => new UserDto(user.FirstName, user.MiddleName, user.LastName, user.Email);
}
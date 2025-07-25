using CSM.Core.Features.Users;
using CSM.UseCases.Dtos.Channels;

namespace CSM.UseCases.Mappers.Users;

public static class DomainToDtoMapper
{
    public static UserDto ToUserDto(this User user) 
        => new(user.FirstName, user.MiddleName, user.LastName, user.Email);
}
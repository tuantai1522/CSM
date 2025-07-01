using CSM.Core.Features.Users;

namespace CSM.UseCases.Abstractions.Authentication;

/// <summary>
/// To create Jwt token for user.
/// </summary>
public interface ITokenProvider
{
    string Create(User user);
}
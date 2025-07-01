namespace CSM.Application.Abstractions.Authentication;

/// <summary>
/// To get UserId from jwt token.
/// </summary>
public interface IUserContext
{
    Guid UserId { get; }
}
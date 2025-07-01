namespace CSM.Application.Abstractions.Authentication;

/// <summary>
/// To define service to hash and verify password from user input.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// To hash password based on password provided by user
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    string Hash(string password);

    /// <summary>
    /// To verify password whether it matches actual password of user
    /// </summary>
    /// <returns></returns>
    bool Verify(string password, string passwordHash);
}
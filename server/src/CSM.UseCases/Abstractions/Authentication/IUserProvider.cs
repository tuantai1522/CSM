using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;

namespace CSM.UseCases.Abstractions.Authentication;

public interface IUserProvider
{
    /// <summary>
    /// Get language from Accept-Language, Ex: "vi"
    /// </summary>
    string GetLocaleFromHeader();
    
    /// <summary>
    /// Get timezone from X-Timezone header, Ex: "Asia/Ho_Chi_Minh"
    /// </summary>
    string GetTimeZoneFromHeader();

    /// <summary>
    /// Get language from header, Ex: "en-US"
    /// </summary>
    /// <returns></returns>
    string GetLanguageFromHeader();
    
    /// <summary>
    /// Get UserId from Jwt token.
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// Get Error from header language
    /// </summary>
    Task<Error> Error(string errorCode, ErrorType errorType);
}
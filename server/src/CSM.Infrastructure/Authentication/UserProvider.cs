using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Application;
using CSM.UseCases.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CSM.Infrastructure.Authentication;

public sealed class UserProvider(IHttpContextAccessor httpContextAccessor, IErrorMessageRepository errorMessageRepository, ITransformer transformer) : IUserProvider
{
    public string GetLocaleFromHeader()
    {
        var locale = httpContextAccessor.HttpContext?.Request.Headers["X-Locale"].ToString();
        return !string.IsNullOrWhiteSpace(locale) ? locale : "en-US";
    }

    public string GetTimeZoneFromHeader()
    {
        var timezone = httpContextAccessor.HttpContext?.Request.Headers["X-Timezone"].ToString();
        return !string.IsNullOrWhiteSpace(timezone) ? timezone : "UTC";
    }

    public string GetLanguageFromHeader()
    {
        var header = httpContextAccessor.HttpContext?.Request.Headers.AcceptLanguage.ToString();
        return header?.Split(',').FirstOrDefault()?.Split('-').FirstOrDefault()?.Trim().ToLowerInvariant() ?? "en";
    }    
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public async Task<Error> Error(string errorCode, ErrorType errorType)
    {
        string languageString = GetLanguageFromHeader();
        
        LanguageType languageType = transformer.FromLanguageStringToEnum(languageString);

        ErrorCode errorCodeEnum = transformer.FromErrorCodeStringToEnum(errorCode);
        
        ErrorMessage errorMessage = await errorMessageRepository.GetErrorMessageByErrorCodeAndLanguageTypeAsync(errorCodeEnum, languageType, CancellationToken.None);

        return new Error(errorCode, errorMessage.Details, errorType);
    }
    

}
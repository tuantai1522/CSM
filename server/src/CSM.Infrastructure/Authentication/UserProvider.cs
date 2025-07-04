using CSM.UseCases.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CSM.Infrastructure.Authentication;

public sealed class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
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
}
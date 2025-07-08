using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Application;

namespace CSM.Infrastructure.Application;

internal sealed class Transformer : ITransformer
{
    public LanguageType FromLanguageStringToEnum(string? language)
    {
        // Return English type
        if (string.IsNullOrEmpty(language))
        {
            return LanguageType.En;
        }
            
        var languageCode = language.Split(',').FirstOrDefault()?.Trim().ToLower();

        return languageCode switch
        {
            "vi" or "vi-vn" => LanguageType.Vn,
            "en" or "en-us" or "en-gb" => LanguageType.En,
            "ja" or "ja-jp" => LanguageType.Ja,
            _ => LanguageType.En
        };        
    }

    public ErrorCode FromErrorCodeStringToEnum(string errorCodeString)
        => Enum.Parse<ErrorCode>(errorCodeString);
}
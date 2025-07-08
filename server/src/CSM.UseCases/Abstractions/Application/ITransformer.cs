using CSM.Core.Features.ErrorMessages;

namespace CSM.UseCases.Abstractions.Application;

public interface ITransformer
{
    public LanguageType FromLanguageStringToEnum(string? language);

    public ErrorCode FromErrorCodeStringToEnum(string errorCodeString);
}
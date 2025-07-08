namespace CSM.Core.Features.ErrorMessages;

public interface IErrorMessageRepository
{
    Task<ErrorMessage> GetErrorMessageByErrorCodeAndLanguageTypeAsync(ErrorCode errorCode, LanguageType languageType, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<ErrorMessage>> GetErrorMessageByErrorCodesAndLanguageTypeAsync(List<ErrorCode> errorCodes, LanguageType languageType, CancellationToken cancellationToken);
}
using CSM.Core.Common;

namespace CSM.Core.Features.ErrorMessages;

public interface IErrorMessageRepository: IRepository<ErrorMessage>
{
    Task<ErrorMessage> GetErrorMessageByErrorCodeAndLanguageTypeAsync(ErrorCode errorCode, LanguageType languageType, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<ErrorMessage>> GetErrorMessageByErrorCodesAndLanguageTypeAsync(List<ErrorCode> errorCodes, LanguageType languageType, CancellationToken cancellationToken);
}
using CSM.Core.Common;

namespace CSM.Core.Features.ErrorMessages;

public sealed class ErrorMessage : IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public ErrorCode ErrorCode { get; private set; } 

    public string Details { get; private set; } = null!;

    public LanguageType LanguageType { get; private set; } = LanguageType.En;

}
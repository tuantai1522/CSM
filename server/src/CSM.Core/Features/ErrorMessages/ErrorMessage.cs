namespace CSM.Core.Features.ErrorMessages;

public sealed class ErrorMessage
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public ErrorCode ErrorCode { get; private set; } 

    public string Details { get; private set; } = null!;

    public LanguageType LanguageType { get; private set; } = LanguageType.En;

}
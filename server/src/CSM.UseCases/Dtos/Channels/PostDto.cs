namespace CSM.UseCases.Dtos.Channels;

public sealed record PostDto(
    Guid MessageId,
    string Message,
    long CreatedAt,
    string NickName);

namespace CSM.UseCases.Features.Channels.GetPostsByChannelId;

public sealed record PostDto(
    Guid MessageId,
    string Message,
    long CreatedAt,
    string NickName);

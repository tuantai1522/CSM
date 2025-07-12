using CSM.UseCases.Dtos.Channels;

namespace CSM.UseCases.Features.Channels.GetPostsByChannelId;

public sealed record GetPostsByChannelIdResponse(
    bool HasMore,
    string? CursorValue,
    IReadOnlyList<PostDto> Posts);

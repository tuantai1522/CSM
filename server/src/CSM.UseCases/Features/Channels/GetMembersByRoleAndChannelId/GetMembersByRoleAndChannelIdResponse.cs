using CSM.UseCases.Dtos.Channels;

namespace CSM.UseCases.Features.Channels.GetMembersByRoleAndChannelId;

public sealed record GetMembersByRoleAndChannelIdResponse(
    int ActivePage,
    int TotalPages,
    int PageSize,
    int TotalUsers,
    IReadOnlyList<UserDto> Users);

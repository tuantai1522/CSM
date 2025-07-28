using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.UseCases.Mappers.Users;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetMembersByRoleAndChannelId;

internal sealed class GetMembersByRoleAndChannelIdQueryHandler(IChannelRepository channelRepository): IRequestHandler<GetMembersByRoleAndChannelIdQuery, Result<GetMembersByRoleAndChannelIdResponse>>
{
    public async Task<Result<GetMembersByRoleAndChannelIdResponse>> Handle(GetMembersByRoleAndChannelIdQuery query, CancellationToken cancellationToken)
    {
        // Get list users
        var users = await channelRepository.GetUsersByRoleAndChannelIdAsync(query.ChannelId, query.IsOwner, query.Page, query.PageSize, cancellationToken);
        
        // Get total users
        var totalUsers = await channelRepository.CountUsersByRoleAndChannelIdAsync(query.ChannelId, query.IsOwner, cancellationToken);

        // Get total pages
        int totalPages = (int)Math.Ceiling((double)totalUsers / query.PageSize);
        
        return new GetMembersByRoleAndChannelIdResponse(
            ActivePage: query.Page,
            TotalPages: totalPages,
            PageSize: query.PageSize,
            TotalUsers: totalUsers,
            Users: users.Select(user => user.ToUserDto()).ToList()
        );
    }
}

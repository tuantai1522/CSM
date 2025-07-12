using CSM.Core.Common;
using CSM.UseCases.Features.Channels.GetPostsByChannelId;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetMembersByRoleAndChannelId;

public sealed record GetMembersByRoleAndChannelIdQuery(Guid ChannelId, bool? IsOwner, int Page = 1, int PageSize = 25) : IRequest<Result<GetMembersByRoleAndChannelIdResponse>>;

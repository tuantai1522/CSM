using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetPostsByChannelId;

public sealed record GetPostsByChannelIdQuery(Guid ChannelId, string? CursorValue, bool IsScrollUp = true, int PageSize = 25) : IRequest<Result<GetPostsByChannelIdResponse>>;

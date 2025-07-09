using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetChannelsByUserId;

public sealed record GetChannelsByUserIdQuery : IRequest<Result<List<GetChannelsByUserIdResponse>>>;

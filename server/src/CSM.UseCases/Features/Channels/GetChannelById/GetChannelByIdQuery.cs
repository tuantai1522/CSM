using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetChannelById;

public sealed record GetChannelByIdQuery(Guid Id) : IRequest<Result<GetChannelByIdResponse>>;

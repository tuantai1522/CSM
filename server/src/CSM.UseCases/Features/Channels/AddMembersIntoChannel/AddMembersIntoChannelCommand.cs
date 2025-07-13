using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.AddMembersIntoChannel;

public sealed record AddMembersIntoChannelCommand(
    Guid Id,
    IReadOnlyList<Guid>? MemberIds,
    IReadOnlyList<Guid>? OwnerIds) : IRequest<Result<Guid>>;

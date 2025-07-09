using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.CreateChannel;

public sealed record CreateChannelCommand(
    string DisplayName, 
    string? Purpose, 
    IReadOnlyList<Guid>? OwnerIds,
    IReadOnlyList<Guid>? MemberIds) : IRequest<Result<Guid>>;

using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Channels.UpdateChannel;

public sealed record UpdateChannelCommand(Guid Id, string DisplayName, string? Purpose) : IRequest<Result<Guid>>;

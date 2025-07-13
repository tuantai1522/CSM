using CSM.UseCases.Features.Channels.AddMembersIntoChannel;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class AddMembersIntoChannel : IEndpoint
{
    private sealed record Request(
        IReadOnlyList<Guid>? OwnerIds,
        IReadOnlyList<Guid>? MemberIds);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("channels/{id:guid}/members", async (
            Guid id,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new AddMembersIntoChannelCommand(id, request.MemberIds, request.OwnerIds);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

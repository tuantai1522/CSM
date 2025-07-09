using CSM.UseCases.Features.Channels;
using CSM.UseCases.Features.Channels.CreateChannel;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class CreateChannel : IEndpoint
{
    private sealed record Request(string DisplayName, 
        string? Purpose, 
        IReadOnlyList<Guid>? OwnerIds,
        IReadOnlyList<Guid>? MemberIds);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("channels", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new CreateChannelCommand(request.DisplayName, request.Purpose, request.OwnerIds, request.MemberIds);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

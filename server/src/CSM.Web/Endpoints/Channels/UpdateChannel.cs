using CSM.UseCases.Features.Channels.UpdateChannel;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class UpdateChannel : IEndpoint
{
    private sealed record Request(
        string DisplayName, 
        string? Purpose);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("channels/{id:guid}", async (
            Guid id,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new UpdateChannelCommand(id, request.DisplayName, request.Purpose);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

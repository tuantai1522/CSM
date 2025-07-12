using CSM.UseCases.Features.Channels.GetPostsByChannelId;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class GetPostsByChannelId : IEndpoint
{
    private sealed record Request(string? CursorValue, 
        bool IsScrollUp = true, 
        int PageSize = 25);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("channels/{id:guid}/posts", async (
            Guid id,
            [AsParameters] Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetPostsByChannelIdQuery(id, request.CursorValue, request.IsScrollUp, request.PageSize);

                var result = await mediator.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

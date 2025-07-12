using CSM.UseCases.Features.Channels.GetMembersByRoleAndChannelId;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class GetMembersByRoleAndChannelId : IEndpoint
{
    private sealed record Request(bool? IsOwner, int Page, int PageSize = 25);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("channels/{id:guid}/members", async (
            Guid id,
            [AsParameters] Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetMembersByRoleAndChannelIdQuery(id, request.IsOwner, request.Page, request.PageSize);

                var result = await mediator.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

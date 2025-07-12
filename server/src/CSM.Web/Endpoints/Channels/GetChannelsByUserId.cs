using CSM.UseCases.Features.Channels.GetChannelsByUserId;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class GetChannelsByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("channels", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetChannelsByUserIdQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Channels)
            .RequireAuthorization();
    }
}

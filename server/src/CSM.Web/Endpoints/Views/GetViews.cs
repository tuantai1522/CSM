using CSM.UseCases.Features.Views.GetViews;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Views;

internal sealed class GetViews : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("views/get-views", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetViewsQuery();

                var result = await mediator.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Views);
    }
}

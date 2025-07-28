using CSM.UseCases.Features.Views.GetViewPermissions;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Views;

internal sealed class GetViewPermissions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("views/get-user-permissions", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetViewPermissionsQuery();

                var result = await mediator.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Views)
        .RequireAuthorization();
    }
}

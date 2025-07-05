using CSM.UseCases.Features.Countries.AddCountry;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class AddCountry : IEndpoint
{
    private sealed record Request(string Name);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("countries", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new AddCountryCommand(request.Name);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}

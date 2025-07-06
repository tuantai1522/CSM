using CSM.UseCases.Features.Countries.AddCity;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class AddCity : IEndpoint
{
    private sealed record Request(string Name, Guid CountryId);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("cities", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new AddCityCommand(request.Name, request.CountryId);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}

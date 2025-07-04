using CSM.UseCases.Features.Countries.GetCountries;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class GetCountries : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("countries/get-countries", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetCountriesQuery();

                var result = await mediator.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries);
    }
}

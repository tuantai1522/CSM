using CSM.UseCases.Features.Countries.GetCitiesByCountryId;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class GetCitiesByCountryId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("countries/get-cities/{countryId:guid}", async (
                Guid countryId,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCitiesByCountryIdQuery(countryId);

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Countries);
    }
}

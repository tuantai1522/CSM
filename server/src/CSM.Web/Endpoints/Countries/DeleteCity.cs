using CSM.UseCases.Features.Countries.DeleteCity;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class DeleteCity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("countries/{id:guid}/cities/{cityId:guid}", async (
            Guid id,
            Guid cityId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new DeleteCityCommand(id, cityId);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}

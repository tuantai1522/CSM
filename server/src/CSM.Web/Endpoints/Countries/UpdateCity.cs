using CSM.UseCases.Features.Countries.UpdateCity;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Countries;

internal sealed class UpdateCity : IEndpoint
{
    private sealed record Request(string Name);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("countries/{id:guid}/cities/{cityId:guid}", async (
            Guid id,
            Guid cityId,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new UpdateCityCommand(id, request.Name, cityId);

                var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}

using CSM.UseCases.Dtos.Views;
using CSM.UseCases.Features.Views.AddViewPermissionsForRole;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Views;

internal sealed class AddViewPermissionsForRole : IEndpoint
{
    private sealed record Request(IReadOnlyList<ViewPermissionDto> Permissions)
    {
    }


    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("views/roles/{roleId:guid}", async (
                Guid roleId,
                Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new AddViewPermissionsForRoleCommand(roleId, request.Permissions);
                
                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Views)
            .RequireAuthorization();

    }
}
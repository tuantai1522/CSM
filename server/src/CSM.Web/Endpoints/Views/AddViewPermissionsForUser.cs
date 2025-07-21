using CSM.UseCases.Dtos.Views;
using CSM.UseCases.Features.Views.AddViewPermissionsForUser;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Views;

internal sealed class AddViewPermissionsForUser : IEndpoint
{
    private sealed record Request(IReadOnlyList<ViewPermissionDto> Permissions)
    {
    }


    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("views/users/{userId:guid}", async (
                Guid userId,
                Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new AddViewPermissionsForUserCommand(userId, request.Permissions);
                
                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Views);
    }
}
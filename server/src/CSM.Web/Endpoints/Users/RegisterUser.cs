using CSM.Core.Features.Users;
using CSM.UseCases.Features.Users.RegisterUser;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Users;

internal sealed class RegisterUser : IEndpoint
{
    private sealed record Request(
        string NickName,
        string FirstName,
        string? MiddleName,
        string? LastName,
        string Email,
        string Password,
        GenderType GenderType,
        Guid CityId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register-user", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(request.NickName,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Email,
                request.Password,
                request.GenderType,
                request.CityId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}

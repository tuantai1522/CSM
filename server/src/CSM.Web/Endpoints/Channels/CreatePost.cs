using CSM.UseCases.Features.Channels.CreatePost;
using CSM.Web.Extensions;
using CSM.Web.Infrastructure;
using MediatR;

namespace CSM.Web.Endpoints.Channels;

internal sealed class CreatePost : IEndpoint
{
    private sealed record Request(Guid? RootId, string Message);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("channels/{id:guid}/posts", async (
            Guid id,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var command = new CreatePostCommand(id, request.RootId, request.Message);

                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Channels)
        .RequireAuthorization();
    }
}

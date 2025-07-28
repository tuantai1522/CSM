using CSM.Core.Features.Views;
using CSM.Web.Attributes;

namespace CSM.Web.Endpoints.Views;

internal sealed class TestAuthorizationView : IEndpoint
{
    // Todo: For testing
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("views/test-authorization-views", () => Results.Ok("Access granted"))
            .WithTags(Tags.Views)
            .RequireAuthorization()
            .AddEndpointFilter((context, next) =>
            {
                // This API needs have ViewCode.CategoriesDocument and ActionType.Read to access resources
                var filter = new BinaryAuthorizeFilter(ViewCode.CategoriesDocument, ActionType.Read);
                return filter.InvokeAsync(context, next);
            });    
    }
}

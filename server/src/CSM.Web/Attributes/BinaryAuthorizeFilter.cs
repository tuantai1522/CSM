using System.Security.Claims;
using CSM.Core.Features.Roles;
using CSM.Core.Features.Views;

namespace CSM.Web.Attributes;

public class BinaryAuthorizeFilter(ViewCode? viewCode, ActionType actionType) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        bool canAccess = await CanAccessToAction(context.HttpContext);

        if (!canAccess)
        {
            return Results.Forbid();
        }
        
        return await next(context);
    }
    
    private async Task<bool> CanAccessToAction(HttpContext httpContext)
    {
        if (httpContext.User.Identity is { IsAuthenticated: false })
        {
            return false;
        }

        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null || !Guid.TryParse(userId, out var parsedUserId))
        {
            throw new ApplicationException("UserId is invalid or not found.");
        }

        if (!viewCode.HasValue)
        {
            return true;
        }
        
        var roleRepository = httpContext.RequestServices.GetService<IRoleRepository>();
        var viewRepository = httpContext.RequestServices.GetService<IViewRepository>();

        if (roleRepository == null || viewRepository == null)
        {
            return false;
        }
        
        var view = await viewRepository.GetViewByViewCodeAsync(viewCode.Value, CancellationToken.None);

        if (view == null)
        {
            return true;
        }
        
        // Get roles of user
        var roles = await roleRepository.GetRolesByUserIdAsync(parsedUserId, CancellationToken.None);
        var roleIds = roles.Select(role => role.Id).ToList();
        
        // Get user permissions of user
        var userViewPermissions = await viewRepository.GetUserPermissionsByUserIdAndViewIdAsync(parsedUserId, view.Id, CancellationToken.None);
        
        // Get role permissions of user
        var roleViewPermissions = await viewRepository.GetRolePermissionsByRoleIdsAndViewIdAsync(roleIds, view.Id, CancellationToken.None);
        
        int actionValue = (int)actionType;
        
        // Combine the user and role permissions using bitwise OR to get the final permission value
        int permissionValues = userViewPermissions
            .Select(up => up.PermissionValue)
            .Concat(roleViewPermissions.Select(rp => rp.PermissionValue))
            .Aggregate(0, (acc, permission) => acc | permission); // Bitwise OR to combine permission values

        return (permissionValues & actionValue) == actionValue;
    }


}
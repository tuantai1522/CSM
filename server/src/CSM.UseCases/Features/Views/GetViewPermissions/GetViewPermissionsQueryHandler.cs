using CSM.Core.Common;
using CSM.Core.Features.Roles;
using CSM.Core.Features.Views;
using CSM.UseCases.Abstractions.Authentication;
using CSM.UseCases.Dtos.Views;
using CSM.UseCases.Mappers.Views;
using MediatR;

namespace CSM.UseCases.Features.Views.GetViewPermissions;

public class GetViewPermissionsQueryHandler(
    IUserProvider userProvider,
    IRoleRepository roleRepository,
    IViewRepository viewRepository) : IRequestHandler<GetViewPermissionsQuery, Result<List<ViewUserDto>>>
{
    public async Task<Result<List<ViewUserDto>>> Handle(GetViewPermissionsQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;
        
        // Get roles of user
        var roles = await roleRepository.GetRolesByUserIdAsync(userId, cancellationToken);
        var roleIds = roles.Select(role => role.Id).ToList();
        
        // Get user permissions of user
        var userPermissions = await viewRepository.GetUserPermissionsByUserIdAsync(userId, cancellationToken);
        
        // Get role permissions of user
        var rolePermissions = await viewRepository.GetRolePermissionsByRoleIdsAsync(roleIds, cancellationToken);
        
        // Get current views of system
        var views = await viewRepository.GetViewsAsync(cancellationToken);

        var viewUsers = views
            .Select(view => view.ToViewUserDto(userPermissions, rolePermissions))
            .ToList();

        return Result.Success(viewUsers);
    }
}
using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.Core.Features.Roles;
using CSM.Core.Features.Views;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Views.AddViewPermissionsForRole;

internal sealed class AddViewPermissionsForRoleCommandHandler(
    IUserProvider userProvider,
    IRoleRepository roleRepository,
    IViewRepository viewRepository): IRequestHandler<AddViewPermissionsForRoleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddViewPermissionsForRoleCommand command, CancellationToken cancellationToken)
    {
        var existedRole = await roleRepository.VerifyExistedRoleIdAsync(command.RoleId, cancellationToken);
        if (!existedRole)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }
        
        var viewIds = command.ViewPermissions.Select(x => x.ViewId).ToList();
        
        var views = await viewRepository.GetViewsByIdsAsync(
            viewIds, 
            cancellationToken,
            
            // Include role permissions
            x => x.RolePermissions.Where(ch => ch.RoleId == command.RoleId));

        foreach (var viewPermission in command.ViewPermissions)
        {
            var view = views.FirstOrDefault(x => x.Id == viewPermission.ViewId);

            var result = view?.AddViewPermissionsForRole(command.RoleId, Convert.ToInt32(viewPermission.PermissionValue, 2));
            if (result is { IsFailure: true })
            {
                return Result.Failure<Guid>(await userProvider.Error(result.Error.Code, result.Error.Type));
            }
        }
        
        await viewRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result.Success(command.RoleId);
    }
}

using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.Core.Features.Users;
using CSM.Core.Features.Views;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Views.AddViewPermissionsForUser;

internal sealed class AddViewPermissionsForRoleCommandHandler(
    IUserProvider userProvider,
    IUserRepository userRepository,
    IViewRepository viewRepository): IRequestHandler<AddViewPermissionsForUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddViewPermissionsForUserCommand command, CancellationToken cancellationToken)
    {
        var existedUser = await userRepository.VerifyExistedUserIdAsync(command.UserId, cancellationToken);
        
        if (!existedUser)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }
        
        var viewIds = command.ViewPermissions.Select(x => x.ViewId).ToList();
        
        var views = await viewRepository.GetViewsByIdsAsync(
            viewIds, 
            cancellationToken,
            
            // Include role permissions
            x => x.UserPermissions.Where(ch => ch.UserId == command.UserId));

        foreach (var viewPermission in command.ViewPermissions)
        {
            var view = views.FirstOrDefault(x => x.Id == viewPermission.ViewId);

            var result = view?.AddViewPermissionsForUser(command.UserId, Convert.ToInt32(viewPermission.PermissionValue, 2));
            if (result is { IsFailure: true })
            {
                return Result.Failure<Guid>(await userProvider.Error(result.Error.Code, result.Error.Type));
            }
        }
        
        await viewRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return Result.Success(command.UserId);
    }
}

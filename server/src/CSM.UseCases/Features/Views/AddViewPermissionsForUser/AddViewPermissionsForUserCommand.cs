using CSM.Core.Common;
using CSM.UseCases.Dtos.Views;
using MediatR;

namespace CSM.UseCases.Features.Views.AddViewPermissionsForUser;

public sealed record AddViewPermissionsForUserCommand(Guid UserId, IReadOnlyList<ViewPermissionDto> ViewPermissions) : IRequest<Result<Guid>>;


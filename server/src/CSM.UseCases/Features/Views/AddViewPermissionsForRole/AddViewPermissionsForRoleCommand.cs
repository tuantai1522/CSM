using CSM.Core.Common;
using CSM.UseCases.Dtos.Views;
using MediatR;

namespace CSM.UseCases.Features.Views.AddViewPermissionsForRole;

public sealed record AddViewPermissionsForRoleCommand(Guid RoleId, IReadOnlyList<ViewPermissionDto> ViewPermissions) : IRequest<Result<Guid>>;


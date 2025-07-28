using CSM.Core.Common;
using CSM.UseCases.Dtos.Views;
using MediatR;

namespace CSM.UseCases.Features.Views.GetViewPermissions;

public sealed record GetViewPermissionsQuery() : IRequest<Result<List<ViewUserDto>>>;

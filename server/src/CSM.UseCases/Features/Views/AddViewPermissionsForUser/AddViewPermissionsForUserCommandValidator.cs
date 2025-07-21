using CSM.Core.Features.ErrorMessages;
using CSM.Core.Features.Views;
using FluentValidation;

namespace CSM.UseCases.Features.Views.AddViewPermissionsForUser;

internal sealed class AddViewPermissionsForRoleCommandValidator : AbstractValidator<AddViewPermissionsForUserCommand>
{
    public AddViewPermissionsForRoleCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithErrorCode(ErrorCode.UserIdEmpty.ToString());
        
        RuleFor(c => c.ViewPermissions)
            .NotEmpty().WithErrorCode(ErrorCode.ListViewEmpty.ToString());

        RuleForEach(c => c.ViewPermissions)
            // Rule 1: Every permission value must be '0' or '1'
            .Must(entry => entry.PermissionValue.All(ch => ch is '0' or '1'))
            .WithErrorCode(ErrorCode.InvalidPermissionsValue.ToString())

            // Rule 2: value must match ActionType enum length
            .Must(entry => entry.PermissionValue.Length == Enum.GetValues<ActionType>().Length)
            .WithErrorCode(ErrorCode.InvalidPermissionsValue.ToString());
    }
}

using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Users.LogIn;

internal sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(ErrorCode.EmailEmpty.ToString())
            .EmailAddress().WithErrorCode(ErrorCode.InvalidEmail.ToString());

        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(ErrorCode.PasswordEmpty.ToString());
    }
}

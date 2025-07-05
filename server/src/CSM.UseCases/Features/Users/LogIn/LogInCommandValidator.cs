using FluentValidation;

namespace CSM.UseCases.Features.Users.LogIn;

internal sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}

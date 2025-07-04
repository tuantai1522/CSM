using FluentValidation;

namespace CSM.UseCases.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.NickName)
            .NotEmpty()
            .WithMessage("Nick name is required.");
        
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        
        RuleFor(c => c.CityId)
            .NotEmpty()
            .WithMessage("CityId is required.");
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}

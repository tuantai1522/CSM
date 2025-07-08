using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.NickName)
            .NotEmpty().WithErrorCode(ErrorCode.NickNameEmpty.ToString());
        
        RuleFor(c => c.FirstName)
            .NotEmpty().WithErrorCode(ErrorCode.FirstNameEmpty.ToString());
        
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(ErrorCode.EmailEmpty.ToString());
        
        RuleFor(c => c.CityId)
            .NotEmpty().WithErrorCode(ErrorCode.CityIdEmpty.ToString());
        
        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(ErrorCode.PasswordEmpty.ToString());
    }
}

using CSM.Core.Common;
using CSM.Core.Features.Users;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserProvider userProvider,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher): IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var verifyEmail = await userRepository.VerifyExistedEmailAsync(command.Email, cancellationToken);

        if (verifyEmail)
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = User.CreateUser(command.NickName, 
            command.FirstName,
            command.MiddleName, 
            command.LastName, 
            command.Email, 
            passwordHasher.Hash(command.Password),
            command.GenderType, 
            command.CityId,
            userProvider.GetTimeZoneFromHeader(), 
            userProvider.GetLocaleFromHeader());
        
        await userRepository.AddUserAsync(user, cancellationToken);

        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(user.Id);
    }
}

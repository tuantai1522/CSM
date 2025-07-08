using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;
using CSM.Core.Features.Users;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Users.LogIn;

internal sealed class LogInCommandHandler(
    IUserProvider userProvider,
    ITokenProvider tokenProvider,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher): IRequestHandler<LogInCommand, Result<LogInResponse>>
{
    public async Task<Result<LogInResponse>> Handle(LogInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindUserByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LogInResponse>(await userProvider.Error(ErrorCode.NotFoundByEmail.ToString(), ErrorType.NotFound));
        }
        
        bool verified = passwordHasher.Verify(command.Password, user.HashPassword);

        if (!verified)
        {
            return Result.Failure<LogInResponse>(await userProvider.Error(ErrorCode.NotFoundByEmail.ToString(), ErrorType.NotFound));
        }

        string accessToken = tokenProvider.Create(user);

        return Result.Success(new LogInResponse(accessToken));
    }
}

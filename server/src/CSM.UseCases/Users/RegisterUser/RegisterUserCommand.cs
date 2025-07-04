using CSM.Core.Common;
using CSM.Core.Features.Users;
using MediatR;

namespace CSM.UseCases.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string NickName,
    string FirstName,
    string? MiddleName,
    string? LastName,
    string Email, 
    string Password,
    GenderType GenderType,
    Guid CityId) : IRequest<Result<Guid>>;

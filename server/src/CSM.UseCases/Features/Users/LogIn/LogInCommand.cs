using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Users.LogIn;

public sealed record LogInCommand(string Email, string Password) : IRequest<Result<LogInResponse>>;

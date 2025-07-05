using CSM.Core.Common;
using CSM.UseCases.Features.Users.LogIn;
using MediatR;

namespace CSM.UseCases.Features.Countries.AddCountry;

public sealed record AddCountryCommand(string Name) : IRequest<Result<Guid>>;

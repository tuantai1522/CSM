using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Countries.AddCity;

public sealed record AddCityCommand(string Name, Guid CountryId) : IRequest<Result<Guid>>;

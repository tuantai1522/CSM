using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Countries.UpdateCity;

public sealed record UpdateCityCommand(Guid Id, string Name, Guid CityId) : IRequest<Result<Guid>>;

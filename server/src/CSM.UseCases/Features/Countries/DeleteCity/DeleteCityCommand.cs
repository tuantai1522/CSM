using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Countries.DeleteCity;

public sealed record DeleteCityCommand(Guid Id, Guid CityId) : IRequest<Result<Guid>>;

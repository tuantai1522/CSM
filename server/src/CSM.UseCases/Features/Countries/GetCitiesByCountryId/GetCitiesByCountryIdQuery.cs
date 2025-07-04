using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

public sealed record GetCitiesByCountryIdQuery(Guid CountryId) : IRequest<Result<List<CityResponse>>>;

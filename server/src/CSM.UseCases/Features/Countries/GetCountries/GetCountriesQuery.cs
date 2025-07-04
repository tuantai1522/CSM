using CSM.Core.Common;
using MediatR;

namespace CSM.UseCases.Features.Countries.GetCountries;

public sealed record GetCountriesQuery : IRequest<Result<List<CountryResponse>>>;
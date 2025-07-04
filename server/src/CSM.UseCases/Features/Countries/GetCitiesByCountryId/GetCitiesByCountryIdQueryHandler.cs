using CSM.Core.Common;
using CSM.Core.Features.Countries;
using MediatR;

namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

public class GetCitiesByCountryIdQueryHandler(ICountryRepository countryRepository) : IRequestHandler<GetCitiesByCountryIdQuery, Result<List<CityResponse>>>
{
    public async Task<Result<List<CityResponse>>> Handle(GetCitiesByCountryIdQuery query, CancellationToken cancellationToken)
    {
        var countries = await countryRepository.GetCitiesByCountryId(query.CountryId, cancellationToken);

        var result = countries
            .Select(x => x.ToCityResponse())
            .ToList();

        return Result.Success(result);
    }
}
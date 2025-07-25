using CSM.Core.Common;
using CSM.Core.Features.Countries;
using CSM.UseCases.Mappers.Countries;
using MediatR;

namespace CSM.UseCases.Features.Countries.GetCountries;

public class GetCountriesQueryHandler(ICountryRepository countryRepository) : IRequestHandler<GetCountriesQuery, Result<List<CountryResponse>>>
{
    public async Task<Result<List<CountryResponse>>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await countryRepository.GetCountriesAsync(cancellationToken);

        var result = countries
            .Select(x => x.ToCountryResponse())
            .ToList();

        return Result.Success(result);
    }
}
using CSM.Core.Features.Countries;
using CSM.UseCases.Features.Countries.GetCitiesByCountryId;
using CSM.UseCases.Features.Countries.GetCountries;

namespace CSM.UseCases.Mappers.Countries;

public static class DomainToDtoMapper
{
    public static CityResponse ToCityResponse(this City city)
        => new(city.Id, city.Name);

    public static CountryResponse ToCountryResponse(this Country country)
        => new(country.Id, country.Name);

}
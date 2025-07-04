using CSM.Core.Features.Countries;

namespace CSM.UseCases.Features.Countries.GetCountries;

public static class CountryMapper
{
    public static CountryResponse ToCountryResponse(this Country country)
        => new()
        {
            Id = country.Id,
            Name = country.Name
        };
}
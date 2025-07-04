using CSM.Core.Features.Countries;

namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

public static class CityMapper
{
    public static CityResponse ToCityResponse(this City city)
        => new()
        {
            Id = city.Id,
            Name = city.Name
        };
}
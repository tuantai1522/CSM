namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

public sealed class CityResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}
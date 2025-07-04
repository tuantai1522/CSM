namespace CSM.UseCases.Features.Countries.GetCountries;

public sealed class CountryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}
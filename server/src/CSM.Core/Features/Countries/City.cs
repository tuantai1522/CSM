namespace CSM.Core.Features.Countries;

public sealed class City
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string Name { get; private set; } = null!;
    
    public Guid CountryId { get; private set; }

    private City()
    {
        
    }

    public static City Create(string name, Guid countryId)
    {
        return new City
        {
            Name = name,
            CountryId = countryId
        };
    }

    public void Update(string cityName)
    {
        Name = cityName;
    }
}
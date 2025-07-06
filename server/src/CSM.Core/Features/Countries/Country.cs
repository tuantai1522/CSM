using CSM.Core.Common;

namespace CSM.Core.Features.Countries;

public sealed class Country : IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string Name { get; private set; } = null!;

    private readonly List<City> _cities = [];
    
    public IReadOnlyList<City> Cities => _cities.ToList();

    private Country()
    {
        
    }

    /// <summary>
    /// Create new country
    /// </summary>
    /// <returns></returns>
    public static Country Create(string name)
    {
        return new Country
        {
            Name = name,
        };
    }

    /// <summary>
    /// Add city into list of cities
    /// </summary>
    /// <param name="city"></param>
    public Result<City> AddCity(City city)
    {
        _cities.Add(city);
        
        return Result.Success(city);
    }

    public Result<City> UpdateCity(Guid cityId, string cityName)
    {
        var city = _cities.FirstOrDefault(x => x.Id == cityId);

        if (city is null)
        {
            return Result.Failure<City>(CountryErrors.CityNotFoundInCountry);
        }

        city.Update(cityName);
        
        return Result.Success(city);
    }
    
    public Result<City> DeleteCity(Guid cityId)
    {
        var city = _cities.FirstOrDefault(x => x.Id == cityId);

        if (city is null)
        {
            return Result.Failure<City>(CountryErrors.CityNotFoundInCountry);
        }

        _cities.Remove(city);
        
        return Result.Success(city);
    }
}
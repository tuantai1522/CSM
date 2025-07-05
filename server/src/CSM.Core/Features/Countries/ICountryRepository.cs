using CSM.Core.Common;

namespace CSM.Core.Features.Countries;

public interface ICountryRepository : IRepository<Country>
{
    public Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<City>> GetCitiesByCountryId(Guid countryId, CancellationToken cancellationToken);

    public Task<Country> AddCountryAsync(Country country, CancellationToken cancellationToken);
}
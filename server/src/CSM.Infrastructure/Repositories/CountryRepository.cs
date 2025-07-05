using CSM.Core.Common;
using CSM.Core.Features.Countries;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class CountryRepository(ApplicationDbContext context) : ICountryRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<City>> GetCitiesByCountryId(Guid countryId, CancellationToken cancellationToken)
    {
        return await _context.Countries
            .AsNoTracking()
            .SelectMany(x => x.Cities)
            .Where(city => city.CountryId == countryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Country> AddCountryAsync(Country country, CancellationToken cancellationToken)
    {
        var result = await _context.Countries.AddAsync(country, cancellationToken);
        
        return result.Entity;
    }
}
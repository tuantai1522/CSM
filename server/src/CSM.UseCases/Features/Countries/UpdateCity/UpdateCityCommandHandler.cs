using CSM.Core.Common;
using CSM.Core.Features.Countries;
using MediatR;

namespace CSM.UseCases.Features.Countries.UpdateCity;

internal sealed class UpdateCityCommandHandler(ICountryRepository countryRepository): IRequestHandler<UpdateCityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCityCommand command, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetCountryByIdAsync(command.CountryId, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Guid>(CountryErrors.NotFoundById);
        }
        
        var city = country.UpdateCity(command.Id, command.Name);

        // If this city is not found in the country, it will return a failure result.
        if (city.IsFailure)
        {
            return Result.Failure<Guid>(city.Error);
        }

        await countryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(city.Value.Id);
    }
}

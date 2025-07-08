using CSM.Core.Common;
using CSM.Core.Features.Countries;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Countries.UpdateCity;

internal sealed class UpdateCityCommandHandler(ICountryRepository countryRepository, IUserProvider userProvider): IRequestHandler<UpdateCityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCityCommand command, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetCountryByIdAsync(command.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }
        
        var city = country.UpdateCity(command.CityId, command.Name);

        // If this city is not found in the country, it will return a failure result.
        if (city.IsFailure)
        {
            return Result.Failure<Guid>(await userProvider.Error(city.Error.Code, city.Error.Type));
        }

        await countryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(city.Value.Id);
    }
}

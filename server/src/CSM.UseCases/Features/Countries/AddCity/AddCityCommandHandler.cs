using CSM.Core.Common;
using CSM.Core.Features.Countries;
using CSM.Core.Features.ErrorMessages;
using CSM.UseCases.Abstractions.Authentication;
using MediatR;

namespace CSM.UseCases.Features.Countries.AddCity;

internal sealed class AddCityCommandHandler(ICountryRepository countryRepository, IUserProvider userProvider): IRequestHandler<AddCityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddCityCommand command, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetCountryByIdAsync(command.CountryId, cancellationToken);

        if (country is null)
        {
            return Result.Failure<Guid>(await userProvider.Error(ErrorCode.NotFoundById.ToString(), ErrorType.NotFound));
        }
        
        var city = City.Create(command.Name, command.CountryId);
        
        country.AddCity(city);

        await countryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(city.Id);
    }
}

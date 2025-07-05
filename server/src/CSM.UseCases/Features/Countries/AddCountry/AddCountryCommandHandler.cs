using CSM.Core.Common;
using CSM.Core.Features.Countries;
using MediatR;

namespace CSM.UseCases.Features.Countries.AddCountry;

internal sealed class AddCountryCommandHandler(ICountryRepository countryRepository): IRequestHandler<AddCountryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddCountryCommand command, CancellationToken cancellationToken)
    {
        var country = Country.Create(command.Name);
        
        var result = await countryRepository.AddCountryAsync(country, cancellationToken);

        await countryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result.Success(result.Id);
    }
}

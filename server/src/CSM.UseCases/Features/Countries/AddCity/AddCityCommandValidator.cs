using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Countries.AddCity;

internal sealed class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    public AddCityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ErrorCode.CityNameEmpty.ToString());
        
        RuleFor(c => c.CountryId)
            .NotEmpty().WithErrorCode(ErrorCode.CountryIdEmpty.ToString());
    }
}

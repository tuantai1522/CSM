using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Countries.DeleteCity;

internal sealed class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    public DeleteCityCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ErrorCode.CountryIdEmpty.ToString());
        
        RuleFor(c => c.CityId)
            .NotEmpty().WithErrorCode(ErrorCode.CityIdEmpty.ToString());
    }
}

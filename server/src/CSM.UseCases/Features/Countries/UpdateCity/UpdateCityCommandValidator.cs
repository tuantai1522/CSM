using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Countries.UpdateCity;

internal sealed class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ErrorCode.CountryIdEmpty.ToString());
        
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ErrorCode.CountryNameEmpty.ToString());
        
        RuleFor(c => c.CityId)
            .NotEmpty().WithErrorCode(ErrorCode.CityIdEmpty.ToString());
    }
}

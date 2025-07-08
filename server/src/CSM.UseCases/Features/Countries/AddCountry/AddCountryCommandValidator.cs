using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Countries.AddCountry;

internal sealed class AddCountryCommandValidator : AbstractValidator<AddCountryCommand>
{
    public AddCountryCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ErrorCode.CountryNameEmpty.ToString());
    }
}

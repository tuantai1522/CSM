using CSM.Core.Features.ErrorMessages;
using FluentValidation;

namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

internal sealed class GetCitiesByCountryIdQueryValidator : AbstractValidator<GetCitiesByCountryIdQuery>
{
    public GetCitiesByCountryIdQueryValidator()
    {
        RuleFor(c => c.CountryId)
            .NotEmpty().WithErrorCode(ErrorCode.CountryIdEmpty.ToString());
    }
}

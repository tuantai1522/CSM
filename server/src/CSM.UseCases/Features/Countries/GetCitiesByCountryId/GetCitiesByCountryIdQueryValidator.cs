using FluentValidation;

namespace CSM.UseCases.Features.Countries.GetCitiesByCountryId;

internal sealed class GetCitiesByCountryIdQueryValidator : AbstractValidator<GetCitiesByCountryIdQuery>
{
    public GetCitiesByCountryIdQueryValidator()
    {
        RuleFor(c => c.CountryId)
            .NotEmpty()
            .WithMessage("CountryId is required.");
    }
}

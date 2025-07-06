using FluentValidation;

namespace CSM.UseCases.Features.Countries.AddCity;

internal sealed class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    public AddCityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(c => c.CountryId)
            .NotEmpty().WithMessage("CountryId is required.");
    }
}

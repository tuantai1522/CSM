using FluentValidation;

namespace CSM.UseCases.Features.Countries.UpdateCity;

internal sealed class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(c => c.CityId)
            .NotEmpty().WithMessage("CityId is required.");
    }
}

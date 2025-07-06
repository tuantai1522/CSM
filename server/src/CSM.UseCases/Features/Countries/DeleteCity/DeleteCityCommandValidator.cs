using FluentValidation;

namespace CSM.UseCases.Features.Countries.DeleteCity;

internal sealed class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    public DeleteCityCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(c => c.CityId)
            .NotEmpty().WithMessage("CityId is required.");
    }
}

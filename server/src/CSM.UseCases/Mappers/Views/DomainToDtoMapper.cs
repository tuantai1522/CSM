using CSM.Core.Features.Views;
using CSM.UseCases.Dtos.Views;

namespace CSM.UseCases.Mappers.Views;

public static class DomainToDtoMapper
{
    public static ViewDto ToViewDto(this View view) 
        => new(view.Id, view.Name, view.Url, view.Views.Select(viewDto => viewDto.ToViewDto()).ToList());
}
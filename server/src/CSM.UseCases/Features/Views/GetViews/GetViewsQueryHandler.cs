using CSM.Core.Common;
using CSM.Core.Features.Views;
using CSM.UseCases.Dtos.Views;
using CSM.UseCases.Mappers.Views;
using MediatR;

namespace CSM.UseCases.Features.Views.GetViews;

public class GetViewsQueryHandler(IViewRepository viewRepository) : IRequestHandler<GetViewsQuery, Result<List<ViewDto>>>
{
    public async Task<Result<List<ViewDto>>> Handle(GetViewsQuery query, CancellationToken cancellationToken)
    {
        var views = await viewRepository.GetViewsAsync(cancellationToken);

        var result = views
            .Select(x => x.ToViewDto())
            .ToList();

        return Result.Success(result);
    }
}
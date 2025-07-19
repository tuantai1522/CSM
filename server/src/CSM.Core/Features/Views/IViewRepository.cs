using CSM.Core.Common;

namespace CSM.Core.Features.Views;

public interface IViewRepository : IRepository<View>
{
    Task<View> AddViewAsync(View view, CancellationToken cancellationToken);
    Task AddViewsAsync(IReadOnlyList<View> views, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedViewDataAsync(CancellationToken cancellationToken);
}
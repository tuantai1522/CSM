using System.Linq.Expressions;
using CSM.Core.Common;

namespace CSM.Core.Features.Views;

public interface IViewRepository : IRepository<View>
{
    Task<View> AddViewAsync(View view, CancellationToken cancellationToken);
    Task AddViewsAsync(IReadOnlyList<View> views, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedViewDataAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<View>> GetViewsByIdsAsync(IReadOnlyList<int> viewIds, CancellationToken cancellationToken, params Expression<Func<View, object>>[]? includeProperties);
    Task<IReadOnlyList<View>> GetViewsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<UserPermission>> GetUserPermissionsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<RolePermission>> GetRolePermissionsByRoleIdsAsync(IReadOnlyList<Guid> roleIds, CancellationToken cancellationToken);
    Task<View?> GetViewByViewCodeAsync(ViewCode viewCode, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<UserPermission>> GetUserPermissionsByUserIdAndViewIdAsync(Guid userId, int viewId, CancellationToken cancellationToken);
    Task<IReadOnlyList<RolePermission>> GetRolePermissionsByRoleIdsAndViewIdAsync(IReadOnlyList<Guid> roleIds, int viewId, CancellationToken cancellationToken);


}
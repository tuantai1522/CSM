using System.Linq.Expressions;
using CSM.Core.Common;
using CSM.Core.Features.Views;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class ViewRepository(ApplicationDbContext context) : IViewRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<View> AddViewAsync(View view, CancellationToken cancellationToken)
    {
        var result = await _context.Views.AddAsync(view, cancellationToken);
        
        return result.Entity;
        
    }

    public async Task AddViewsAsync(IReadOnlyList<View> views, CancellationToken cancellationToken)
    {
        await _context.Views.AddRangeAsync(views, cancellationToken);
    }

    public async Task<bool> VerifyExistedViewDataAsync(CancellationToken cancellationToken)
        => await _context.Views.AnyAsync(cancellationToken);

    public async Task<IReadOnlyList<View>> GetViewsByIdsAsync(IReadOnlyList<int> viewIds, CancellationToken cancellationToken, params Expression<Func<View, object>>[]? includeProperties)
    {
        var query = _context.Views.AsSplitQuery().Where(x => viewIds.Contains(x.Id));

        // Apply the include logic dynamically using the provided Func
        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<UserPermission>> GetUserPermissionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Views
            .Where(view => view.UserPermissions.Any(userPermission => userPermission.UserId == userId))
            .SelectMany(x => x.UserPermissions)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<RolePermission>> GetRolePermissionsByRoleIdsAsync(IReadOnlyList<Guid> roleIds, CancellationToken cancellationToken)
        => await _context.Views
            .Where(view => view.RolePermissions.Any(rolePermission => roleIds.Contains(rolePermission.RoleId)))
            .SelectMany(x => x.RolePermissions)
            .Where(rolePermission => roleIds.Contains(rolePermission.RoleId))
            .ToListAsync(cancellationToken);
    
    public async Task<IReadOnlyList<View>> GetViewsAsync(CancellationToken cancellationToken)
    {
        var views = await _context.Views
            .OrderBy(x => x.SortOrder)           
            .Include(x => x.Views.OrderBy(view => view.SortOrder))               
            .ToListAsync(cancellationToken);
        
        // Only filter parent view
        views = views
            .Where(x => x.ParentViewId == null)
            .ToList();

        return views;
    }
}
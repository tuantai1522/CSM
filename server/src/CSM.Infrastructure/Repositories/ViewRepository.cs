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
}
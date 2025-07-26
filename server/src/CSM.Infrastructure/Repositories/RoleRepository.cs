using CSM.Core.Common;
using CSM.Core.Features.Roles;
using CSM.Core.Features.Views;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken)
    {
        var result = await _context.Roles.AddAsync(role, cancellationToken);
        
        return result.Entity;
        
    }

    public async Task AddRolesAsync(IReadOnlyList<Role> roles, CancellationToken cancellationToken)
    {
        await _context.Roles.AddRangeAsync(roles, cancellationToken);
    }

    public async Task<bool> VerifyExistedRoleDataAsync(CancellationToken cancellationToken)
        => await _context.Roles.AnyAsync(cancellationToken);

    public async Task<bool> VerifyExistedRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
        => await _context.Roles.AnyAsync(x => x.Id == roleId, cancellationToken);

    public async Task<List<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Roles.Where(role => role.UserRoles.Any(userRole => userRole.UserId == userId)).ToListAsync(cancellationToken);

}
using CSM.Core.Common;

namespace CSM.Core.Features.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken);
    Task AddRolesAsync(IReadOnlyList<Role> roles, CancellationToken cancellationToken);
    
    Task<bool> VerifyExistedRoleDataAsync(CancellationToken cancellationToken);
    Task<bool> VerifyExistedRoleIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
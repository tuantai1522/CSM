using CSM.Core.Features.Users;

namespace CSM.Core.Features.Roles;

public sealed class UserRole
{
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    private UserRole()
    {
        
    }
    
    public static UserRole CreateUserRole(Guid roleId, Guid userId)
    {
        return new UserRole
        {
            RoleId = roleId,
            UserId = userId,
        };
    }
}
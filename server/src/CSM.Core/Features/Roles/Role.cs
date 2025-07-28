using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;

namespace CSM.Core.Features.Roles;

public sealed class Role : IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Name { get; private set; } = null!;
    
    public string Code { get; private set; } = null!;
    
    public string? Description { get; private set; }
    
    /// <summary>
    /// List users of this role
    /// </summary>
    private readonly List<UserRole> _userRoles = [];
    
    public IReadOnlyList<UserRole> UserRoles => _userRoles.ToList();
    
    /// <summary>
    /// Todo: Apply SoftDelete interface
    /// </summary>

    private Role()
    {
        
    }
    
    public static Role CreateRole(string name, string code, string description)
    {
        return new Role
        {
            Name = name,
            Code = code,
            Description = description
        };
    }
    
    /// <summary>
    /// Add user into role.
    /// </summary>
    public Result AddUserIntoRole(Guid userId)
    {
        // To verify this user is in role before or not
        var user = _userRoles.FirstOrDefault(x => x.UserId == userId);
        if (user is not null)
        {
            return Result.Failure(Error.DomainError(ErrorCode.ThisUserAlreadyBelongsToThisRole.ToString(), ErrorType.Validation));
        }
        
        var userRole = UserRole.CreateUserRole(Id, userId);

        _userRoles.Add(userRole);
        
        return Result.Success();
    }
}
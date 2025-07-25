using CSM.Core.Common;
using CSM.Core.Features.ErrorMessages;

namespace CSM.Core.Features.Views;

public sealed class View : IAggregateRoot
{
    public int Id { get; private init; }

    public string Name { get; private set; } = null!;
    
    /// <summary>
    /// Code of current view
    /// </summary>
    public ViewCode ViewCode { get; private set; }
    
    public int SortOrder { get; private set; }
    
    public int? ParentViewId { get; private init; }
    
    public string? Url { get; set; }

    /// <summary>
    /// List view children
    /// </summary>
    private readonly List<View> _views = [];
    
    public IReadOnlyList<View> Views => _views.ToList();
    
    /// <summary>
    /// List permission of users
    /// </summary>
    private readonly List<UserPermission> _userPermissions = [];
    
    public IReadOnlyList<UserPermission> UserPermissions => _userPermissions.ToList();
    
    /// <summary>
    /// List permission of roles
    /// </summary>
    private readonly List<RolePermission> _rolePermissions = [];
    
    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.ToList();

    /// <summary>
    /// Todo: Apply ISoftDelete interface
    /// </summary>
    private View()
    {
        
    }

    public static View CreateView(string name, ViewCode viewCode, int sortOrder, string? url, List<View> views)
    {
        var newView = new View
        {
            Name = name,
            ViewCode = viewCode,
            SortOrder = sortOrder,
            Url = url,
        };
        
        // Add children view
        newView._views.AddRange(views);
        
        return newView;
    }

    public void AddViewChildren(string name, ViewCode viewCode, int sortOrder, string? url)
    {
        _views.Add(new View
        {
            Name = name,
            ViewCode = viewCode,
            SortOrder = sortOrder,
            Url = url,
            ParentViewId = Id
        });
    }

    public Result AddViewPermissionsForRole(Guid roleId, int permissionValue)
    {
        if (ParentViewId == null)
        {
            return Result.Failure(Error.DomainError(ErrorCode.CanNotAssignToParentView.ToString(), ErrorType.Validation));
        }
        
        var existingRolePermission = _rolePermissions
            .FirstOrDefault(rp => rp.RoleId == roleId);
        
        // Update new role permission value
        if (existingRolePermission is not null)
        {
            existingRolePermission.UpdatePermissionValue(permissionValue);
        }
        else
        {
            _rolePermissions.Add(RolePermission.CreateRolePermission(Id, roleId, permissionValue));
        }
        return Result.Success();
    }
    
    public Result AddViewPermissionsForUser(Guid userId, int permissionValue)
    {
        if (ParentViewId == null)
        {
            return Result.Failure(Error.DomainError(ErrorCode.CanNotAssignToParentView.ToString(), ErrorType.Validation));
        }
        
        var existingUserPermission = _userPermissions
            .FirstOrDefault(rp => rp.UserId == userId);
        
        // Update new user permission value
        if (existingUserPermission is not null)
        {
            existingUserPermission.UpdatePermissionValue(permissionValue);
        }
        else
        {
            _userPermissions.Add(UserPermission.CreateUserPermission(Id, userId, permissionValue));
        }
        
        return Result.Success();
    }
}
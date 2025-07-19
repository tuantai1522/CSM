using CSM.Core.Common;
namespace CSM.Core.Features.Views;

public sealed class View : IAggregateRoot
{
    public int Id { get; private init; }

    public string Name { get; private set; } = null!;
    
    /// <summary>
    /// Key of current view
    /// </summary>
    public ViewPermission ViewPermission { get; private set; }
    
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

    public static View CreateView(string name, ViewPermission viewPermission, int sortOrder, string? url)
    {
        return new View
        {
            Name = name,
            ViewPermission = viewPermission,
            SortOrder = sortOrder,
            Url = url
        };
    }

    public void AddViewChildren(string name, ViewPermission viewPermission, int sortOrder, string? url)
    {
        _views.Add(new View
        {
            Name = name,
            ViewPermission = viewPermission,
            SortOrder = sortOrder,
            Url = url,
            ParentViewId = Id
        });
    }

    public void AddViewPermissionForRole(Guid roleId, int permissionValue)
    {
        _rolePermissions.Add(RolePermission.CreateRolePermission(Id, roleId, permissionValue));
    }
    
    public void AddViewPermissionForUser(Guid userId, int permissionValue)
    {
        _userPermissions.Add(UserPermission.CreateUserPermission(Id, userId, permissionValue));
    }
}
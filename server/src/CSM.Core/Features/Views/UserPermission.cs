namespace CSM.Core.Features.Views;

public sealed class UserPermission
{
    public int ViewId { get; private set; }

    public Guid UserId { get; private set; }    
    
    public int PermissionValue { get; private set; }

    private UserPermission()
    {
        
    }

    public static UserPermission CreateUserPermission(int viewId, Guid userId, int permissionValue)
    {
        return new UserPermission
        {
            ViewId = viewId,
            UserId = userId,
            PermissionValue = permissionValue
        };
    }
    
    public void UpdatePermissionValue(int permissionValue) => PermissionValue = permissionValue;
}
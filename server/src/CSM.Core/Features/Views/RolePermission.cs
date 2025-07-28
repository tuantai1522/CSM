namespace CSM.Core.Features.Views;

public sealed class RolePermission
{
    public int ViewId { get; private set; }

    public Guid RoleId { get; private set; }    
    
    public int PermissionValue { get; private set; }

    private RolePermission()
    {
        
    }

    public static RolePermission CreateRolePermission(int viewId, Guid roleId, int permissionValue)
    {
        return new RolePermission
        {
            ViewId = viewId,
            RoleId = roleId,
            PermissionValue = permissionValue
        };
    }
    
    public void UpdatePermissionValue(int permissionValue) => PermissionValue = permissionValue;
}
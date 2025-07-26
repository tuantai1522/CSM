using CSM.Core.Features.Views;
using CSM.UseCases.Dtos.Views;

namespace CSM.UseCases.Mappers.Views;

public static class DomainToDtoMapper
{
    public static ViewDto ToViewDto(this View view) 
        => new(view.Id, view.Name, view.Url, view.Views.Select(viewDto => viewDto.ToViewDto()).ToList());
    
    public static ViewUserDto ToViewUserDto(this View view, IReadOnlyList<UserPermission> userPermissions, IReadOnlyList<RolePermission> rolePermissions)
    {
        // Get user and role permissions for this view
        var userViewPermissions = userPermissions.Where(up => up.ViewId == view.Id).ToList();
        var roleViewPermissions = rolePermissions.Where(rp => rp.ViewId == view.Id).ToList();

        // Combine the user and role permissions using bitwise OR to get the final permission value
        int permissionValues = userViewPermissions
            .Select(up => up.PermissionValue)
            .Concat(roleViewPermissions.Select(rp => rp.PermissionValue))
            .Aggregate(0, (acc, permission) => acc | permission); // Bitwise OR to combine permission values

        // Convert the combined permission values to a binary string
        int actionLength = Enum.GetValues<ActionType>().Length;
        var permissionStringValue = Convert.ToString(permissionValues, 2).PadLeft(actionLength, '0');

        // Create DTO for this view with the calculated permission value
        var viewUserDto = new ViewUserDto(view.Id, view.Name, view.Url, view.Views.Any()
                ? view.Views.Select(viewDto => viewDto.ToViewUserDto(userPermissions, rolePermissions)).ToList() // Apply permissions to child views
                : [], // No child views, no recursion
            permissionStringValue
        );

        return viewUserDto;
    }
}
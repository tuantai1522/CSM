namespace CSM.UseCases.Dtos.Views;

public sealed record ViewUserDto(int ViewId, string Name, string? Url, IReadOnlyList<ViewUserDto> Views, string PermissionValue);

namespace CSM.UseCases.Dtos.Views;

public sealed record ViewDto(int ViewId, string Name, string? Url, string PermissionValue, IReadOnlyList<ViewDto> Views);

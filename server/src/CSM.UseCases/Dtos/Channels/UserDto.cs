namespace CSM.UseCases.Dtos.Channels;

public sealed record UserDto(
    string FirstName,
    string? MiddleName,
    string? LastName,
    string? Email);

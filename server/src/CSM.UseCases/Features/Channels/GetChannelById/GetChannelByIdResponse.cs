namespace CSM.UseCases.Features.Channels.GetChannelById;

public sealed record GetChannelByIdResponse(Guid Id, string DisplayName, int CountParticipants);

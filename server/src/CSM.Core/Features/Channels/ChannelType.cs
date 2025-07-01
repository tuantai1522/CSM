namespace CSM.Core.Features.Channels;

public enum ChannelType
{
    Direct = 0, // Direct channel between only 2 users (don't allow to add new members)
    Group = 1, // Group is created by users and can invite other people
}
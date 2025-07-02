using System.Text.Json.Serialization;

namespace CSM.Core.Features.Channels;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChannelType
{
    Direct = 0, // Direct channel between only 2 users (don't allow to add new members)
    Group = 1, // Group is created by users and can invite other people
}
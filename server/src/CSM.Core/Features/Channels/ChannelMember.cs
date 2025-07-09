namespace CSM.Core.Features.Channels;

public sealed class ChannelMember
{
    public Guid ChannelId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public bool IsOwner { get; private set; }
    
    /// <summary>
    /// Time when user is updated by role, ...
    /// </summary>
    public long LastUpdatedAt { get; private set; } 
    
    /// <summary>
    /// The last time the user viewed the channel.
    /// </summary>
    public long LastViewedAt { get; private set; }
    
    /// <summary>
    /// A total of posts in main and channel at time when user viewed the channel.
    /// </summary>
    public long PostCount { get; private set; }
    
    /// <summary>
    /// Time when user was removed from this channel.
    /// </summary>
    public long? DeletedAt { get; private set; }

    private ChannelMember()
    {
        
    }

    /// <summary>
    /// To create instance of channel member.
    /// </summary>
    public static ChannelMember CreateChannelMember(Guid channelId, Guid userId, bool isOwner, long postCount)
    {
        return new ChannelMember
        {
            ChannelId = channelId,
            UserId = userId,
            IsOwner = isOwner,
            LastViewedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            PostCount = postCount,
        };
    }

    /// <summary>
    /// Update value when user viewed the channel.
    /// </summary>
    public void UpdateLastViewedTimeInChannel(long postCount, long lastViewedAt)
    {
        PostCount = postCount;
        LastViewedAt = lastViewedAt;
    }

}
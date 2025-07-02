namespace CSM.Core.Features.Channels;

public class Post
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public Guid ChannelId { get; private set; } 
    public Guid UserId { get; private set; } 
    
    /// <summary>
    /// ID of root post if this was sent in thread
    /// </summary>
    public Guid? RootId { get; private set; }
    
    /// <summary>
    /// Message of post to send
    /// </summary>
    public string Message { get; private set; } = null!;

    /// <summary>
    /// Type of post
    /// </summary>
    public PostType Type { get; private set; } = PostType.Normal;

    public long CreatedAt { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }
    public long? DeletedAt { get; private set; }

    public static Post CreatePost(Guid channelId, Guid userId, Guid? rootId, string message, PostType type = PostType.Normal)
    {
        return new Post
        {
            ChannelId = channelId,
            UserId = userId,
            RootId = rootId,
            Message = message,
            Type = type
        };
    }
}
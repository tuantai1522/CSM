using CSM.Core.Common;
using CSM.Core.Features.Users;

namespace CSM.Core.Features.Channels;

public class Channel : Entity, IAuditableEntity, IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    /// <summary>
    /// ChannelType, default is group.
    /// </summary>
    public ChannelType Type { get; private set; } = ChannelType.Group;

    /// <summary>
    /// Name of channel
    /// </summary>
    public string DisplayName { get; private set; } = null!;

    public string? Purpose { get; private set; }

    /// <summary>
    /// Time when last post was sent in this channel (for sorting purposes).
    /// </summary>
    public long LastPostAt { get; private set; }

    /// <summary>
    /// Total number of posts in this channel.
    /// </summary>
    public long TotalPostCount { get; private set; }

    /// <summary>
    /// A person who created this channel.
    /// </summary>
    public Guid CreatorId { get; private set; }

    public User User { get; private set; } = null!;

    public long CreatedAt { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    public long? UpdatedAt { get; private set; }

    public long? DeletedAt { get; private set; }
    
    /// <summary>
    /// List members of this channel.
    /// </summary>
    private readonly HashSet<ChannelMember> _channelMembers = [];
    
    public IReadOnlyList<ChannelMember> ChannelMembers => _channelMembers.ToList();
    
    /// <summary>
    /// List posts of this channel.
    /// </summary>
    private readonly HashSet<Post> _posts = [];
    
    public IReadOnlyList<Post> Posts => _posts.ToList();

    private Channel()
    {
        
    }

    public static Channel CreateChannel(ChannelType type, string displayName, string? purpose, Guid creatorId)
    {
        return new Channel
        {
            Type = type,
            DisplayName = displayName,
            Purpose = purpose,
            LastPostAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), // for sorting purposes
            CreatorId = creatorId,
        };
    }
    
    /// <summary>
    /// Update total post and when last post was sent in this channel.
    /// </summary>
    public void UpdateLastPostAtAndTotalPostInChannel(long postCount, long lastPostAt)
    {
        LastPostAt = lastPostAt;
        TotalPostCount = postCount;
    }
    
    /// <summary>
    /// Add member into channel.
    /// </summary>
    public void AddMember(Guid userId, bool isOwner)
    {
        // Assign total post count to this channel member
        var channelMember = ChannelMember.CreateChannelMember(Id, userId, isOwner, TotalPostCount);
        _channelMembers.Add(channelMember);
    }

    /// <summary>
    /// Create post int channel
    /// </summary>
    public void CreatePost(Guid userId, Guid? rootId, string message, PostType type = PostType.Normal)
    {
        var post = Post.CreatePost(Id, userId, rootId, message, type);
        _posts.Add(post);
    }

}
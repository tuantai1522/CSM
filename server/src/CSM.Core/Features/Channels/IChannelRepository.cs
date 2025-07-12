using System.Linq.Expressions;
using CSM.Core.Common;
using CSM.Core.Features.Users;

namespace CSM.Core.Features.Channels;

public interface IChannelRepository : IRepository<Channel>
{
    Task<Channel?> GetChannelByIdAsync(Guid channelId, CancellationToken cancellationToken, params Expression<Func<Channel, object>>[]? includeProperties);
    
    Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<Channel>> GetChannelsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<List<Post>> GetPostsByChannelIdAsync(Guid channelId, long? createdAt, Guid? lastId, bool isScrollUp, int pageSize, CancellationToken cancellationToken);

    Task<List<User>> GetUsersByRoleAndChannelIdAsync(Guid channelId, bool? isOwner, int page, int pageSize, CancellationToken cancellationToken);
    Task<int> CountUsersByRoleAndChannelIdAsync(Guid channelId, bool? isOwner, CancellationToken cancellationToken);
}
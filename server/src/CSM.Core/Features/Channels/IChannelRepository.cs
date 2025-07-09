using CSM.Core.Common;

namespace CSM.Core.Features.Channels;

public interface IChannelRepository : IRepository<Channel>
{
    Task<Channel?> GetChannelByIdAsync(Guid channelId, CancellationToken cancellationToken);
    
    public Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken);
}
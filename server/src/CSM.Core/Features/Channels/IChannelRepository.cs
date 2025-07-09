using CSM.Core.Common;

namespace CSM.Core.Features.Channels;

public interface IChannelRepository : IRepository<Channel>
{
    public Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken);
}
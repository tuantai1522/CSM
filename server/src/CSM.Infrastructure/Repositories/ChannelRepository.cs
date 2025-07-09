using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Infrastructure.Database;

namespace CSM.Infrastructure.Repositories;

public sealed class ChannelRepository(ApplicationDbContext context) : IChannelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken)
    {
        var result = await _context.Channels.AddAsync(channel, cancellationToken);

        return result.Entity;
    }
}

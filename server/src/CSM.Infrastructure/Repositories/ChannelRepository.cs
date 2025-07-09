using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class ChannelRepository(ApplicationDbContext context) : IChannelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Channel?> GetChannelByIdAsync(Guid channelId, CancellationToken cancellationToken)
        // Todo: To check this to only include necessary navigation properties
        => await _context.Channels
            .AsSplitQuery()
            .Include(x => x.ChannelMembers.Where(cm => cm.ChannelId == channelId))
            .Include(x => x.Posts.Where(cm => cm.ChannelId == channelId))
            .FirstOrDefaultAsync(x => x.Id == channelId, cancellationToken: cancellationToken);
    
    public async Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken)
    {
        var result = await _context.Channels.AddAsync(channel, cancellationToken);

        return result.Entity;
    }
}

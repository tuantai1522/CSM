using System.Linq.Expressions;
using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CSM.Infrastructure.Repositories;

public sealed class ChannelRepository(ApplicationDbContext context) : IChannelRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Channel?> GetChannelByIdAsync(Guid channelId, CancellationToken cancellationToken, params Expression<Func<Channel, object>>[]? includeProperties)
    {
        var query = _context.Channels.AsSplitQuery().Where(x => x.Id == channelId);

        // Apply the include logic dynamically using the provided Func
        if (includeProperties != null)
        {
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<Channel> AddChannelAsync(Channel channel, CancellationToken cancellationToken)
    {
        var result = await _context.Channels.AddAsync(channel, cancellationToken);

        return result.Entity;
    }
}

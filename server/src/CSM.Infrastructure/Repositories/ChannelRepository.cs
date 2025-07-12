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

    public async Task<Channel?> GetChannelByIdAsync(Guid channelId, CancellationToken cancellationToken,
        params Expression<Func<Channel, object>>[]? includeProperties)
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

    public async Task<IReadOnlyList<Channel>> GetChannelsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Channels
            .Where(x => x.ChannelMembers.Any(ch => ch.UserId == userId))
            .OrderByDescending(x => x.LastPostAt)
            .ToListAsync(cancellationToken);

    public async Task<List<Post>> GetPostsByChannelIdAsync(Guid channelId, long? createdAt, Guid? lastId,
        bool isScrollUp, int pageSize, CancellationToken cancellationToken)
    {
        var query = _context.Channels
            .Where(c => c.Id == channelId)
            .SelectMany(c => c.Posts)
            .AsQueryable();

        if (createdAt.HasValue && lastId.HasValue)
        {
            if (isScrollUp)
            {
                query = query.Where(post => EF.Functions.LessThanOrEqual(
                    ValueTuple.Create(post.CreatedAt, post.Id),
                    ValueTuple.Create(createdAt.Value, lastId.Value)));
            }
            else
            {
                query = query.Where(post => EF.Functions.GreaterThanOrEqual(
                    ValueTuple.Create(post.CreatedAt, post.Id),
                    ValueTuple.Create(createdAt.Value, lastId.Value)));
            }
        }

        query = query
            .OrderByDescending(p => p.CreatedAt)
            .ThenByDescending(p => p.Id);
        
        var result = await query
            .Include(x => x.User)
            .Take(pageSize + 1)
            .ToListAsync(cancellationToken);

        return result;
    }
}

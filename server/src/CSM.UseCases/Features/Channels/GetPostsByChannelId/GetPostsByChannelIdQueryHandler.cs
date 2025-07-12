using CSM.Core.Common;
using CSM.Core.Features.Channels;
using CSM.UseCases.CursorPagination;
using CSM.UseCases.Mappers.Channels;
using MediatR;

namespace CSM.UseCases.Features.Channels.GetPostsByChannelId;

internal sealed class GetPostsByChannelIdQueryHandler(IChannelRepository channelRepository): IRequestHandler<GetPostsByChannelIdQuery, Result<GetPostsByChannelIdResponse>>
{
    public async Task<Result<GetPostsByChannelIdResponse>> Handle(GetPostsByChannelIdQuery query, CancellationToken cancellationToken)
    {
        Cursor? decodedCursor = Cursor.Decode(query.CursorValue);

        var posts = await channelRepository.GetPostsByChannelIdAsync(
            query.ChannelId,
            decodedCursor?.CreatedAt,
            decodedCursor?.LastId,
            query.IsScrollUp,
            query.PageSize,
            cancellationToken);

        (bool hasMore, string? cursorValue) = HandleCursor(decodedCursor, posts, query.PageSize, query.IsScrollUp);

        var result = new GetPostsByChannelIdResponse(
            hasMore,
            cursorValue,
            posts.Select(item => item.ToPostDto()).ToList()
        );

        return Result.Success(result);
    }

    private (bool hasMore, string? cursorValue) HandleCursor(Cursor? decodedCursor, List<Post> posts, int pageSize, bool isScrollUp)
    {
        bool hasMore = posts.Count > pageSize;

        if (decodedCursor is null)
        {
            posts.RemoveAt(posts.Count - 1); 
        }
        else if (hasMore)
        {
            posts.RemoveAt(0); 
        }

        long? nextDate = null;
        Guid? nextId = null;

        if (hasMore)
        {
            Post lastItem = isScrollUp ? posts[^1] : posts[0];

            nextDate = lastItem.CreatedAt;
            nextId = lastItem.Id;
        }

        string? cursorValue = nextDate != null && nextId != null
            ? Cursor.Encode(nextDate.Value, nextId.Value)
            : null;
        
        return (hasMore, cursorValue);
    }
}

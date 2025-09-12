using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetChannelsByOwnerQueryHandler : IRequestHandler<GetChannelsByOwnerQuery, List<GetAllChannelsQueryResult>>
{
    private readonly ChannelDbContext _context;
    public GetChannelsByOwnerQueryHandler(ChannelDbContext context)
    {
        _context = context;
    }
    public async Task<List<GetAllChannelsQueryResult>> Handle(GetChannelsByOwnerQuery request, CancellationToken cancellationToken)
    {
        return await _context.Channels.Where(c => c.OwnerId == request.OwnerId && c.IsActive == true).Select(c => new GetAllChannelsQueryResult
        {
            ChannelId = c.ChannelId,
            ChannelName = c.ChannelName,
            Category = c.Category,
            Subscribers = c.Subscribers
        }).ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;

public class ChannelRepository : IChannelRepository
{
    private readonly ChannelDbContext _context;
    public ChannelRepository(ChannelDbContext context)
    {
        _context = context;
    }
    public async Task<Channel> CreateChannel(Channel entity)
    {
        await _context.Channels.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteChannel(Guid channelId)
    {
        var entity = await _context.Channels.FindAsync(channelId);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Channels.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<GetAllChannelsQueryResult>> GetAllChannels()
    {
        return await _context.Channels.Select(c => new GetAllChannelsQueryResult
        {
            ChannelName = c.ChannelName,
            Subscribers = c.Subscribers
        }).ToListAsync();
    }
    public async Task<List<GetChannelByIdQueryResult>> GetChannelById(Guid channelId)
    {
        return await _context.Channels.Where(c => c.ChannelId == channelId).Select(c => new GetChannelByIdQueryResult
        {
            ChannelId = c.ChannelId,

        }).ToListAsync();
    }

    public async Task<List<GetChannelDetailQueryResult>> GetChannelDetail()
    {
        return await _context.Channels.Select(c => new GetChannelDetailQueryResult
        {
            ChannelId = c.ChannelId,
            ChannelName = c.ChannelName,
            Category = c.Category,
            Subscribers = c.Subscribers,
            IsActive = c.IsActive,
            CreationDate = c.CreationDate,
            SubscriberList = c.ChannelSubscribers
                .Select(s=> new ChannelSubscriberDto
                {
                    ChannelId = s.ChannelId,
                    UserId = s.UserId,
                    UserName = s.UserName,
                    UserLastName = s.UserLastName,
                    SubscribedAt = s.SubscribedAt
                }).ToList()
        }).ToListAsync();
    }

    public async Task<bool> UpdateChannel(UpdateChannelDto dto)
    {
        var entity = await _context.Channels.FindAsync(dto.ChannelId);
        entity.ChannelName = dto.ChannelName;
        entity.Category = dto.Category;
        await _context.SaveChangesAsync();
        return true;
    }

}


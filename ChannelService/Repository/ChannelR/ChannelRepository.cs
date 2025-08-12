using Microsoft.EntityFrameworkCore;

public class ChannelRepository : IChannelRepository
{
    private readonly ChannelDbContext _context;
    public ChannelRepository(ChannelDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateChannel(Channel entity)
    {
        await _context.Channels.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.ChannelId;
    }
    public async Task<bool> DeleteChannel(Guid channelId)
    {
        var entity = await _context.Channels.FindAsync(channelId);
        if (entity == null) return false;
        _context.Channels.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<IReadOnlyList<ChannelListDto>> GetAllChannels()
    {
        return await _context.Channels.Select(c => new ChannelListDto
        {
            ChannelName = c.ChannelName,
            Subscribers = c.Subscribers
        }).ToListAsync();
    }
    public async Task<List<ChannelDetailDto>> GetChannelById(Guid channelId)
    {
        return await _context.Channels.Where(c => c.ChannelId == channelId).Select(c => new ChannelDetailDto
        {
            ChannelId = c.ChannelId,
            ChannelName = c.ChannelName,
            Category = c.Category,
            Subscribers = c.Subscribers,
            IsActive = c.IsActive,

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


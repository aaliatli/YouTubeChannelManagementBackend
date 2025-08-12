public interface IChannelRepository
{
    Task<Guid> CreateChannel(Channel entity);
    Task<bool> DeleteChannel(Guid ChannelId);
    Task<bool> UpdateChannel(UpdateChannelDto dto);
    Task<List<ChannelDetailDto>> GetChannelById(Guid ChannelId);
    Task<IReadOnlyList<ChannelListDto>> GetAllChannels();
}
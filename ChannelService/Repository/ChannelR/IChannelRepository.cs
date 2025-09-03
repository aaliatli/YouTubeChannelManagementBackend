public interface IChannelRepository
{
    Task<Channel> CreateChannel(Channel entity);
    Task<bool> DeleteChannel(Guid ChannelId);
    Task<bool> UpdateChannel(UpdateChannelDto dto);
    Task<List<GetChannelByIdQueryResult>> GetChannelById(Guid ChannelId);
    Task<List<GetChannelDetailQueryResult>> GetChannelDetail();
    Task<List<GetAllChannelsQueryResult>> GetAllChannels();
}
public interface IChannelRepository
{
    Task<Channel> CreateChannel(Channel entity);
    Task<bool> DeleteChannel(Guid ChannelId);
    Task<Channel> UpdateChannel(Channel ch);
    Task<GetChannelByIdQueryResult> GetChannelById(Guid ChannelId);
    Task<List<GetChannelDetailQueryResult>> GetChannelDetail();
    Task<List<GetAllChannelsQueryResult>> GetAllChannels();
}
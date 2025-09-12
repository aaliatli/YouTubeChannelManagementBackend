using MediatR;
public class GetChannelByIdQuery : IRequest<GetChannelByIdQueryResult>, ICacheable
{
    public Guid ChannelId { get; set; }
    public string CacheKey => $"channel_by_id_{ChannelId}";
    public int CacheDuration => 10;
}
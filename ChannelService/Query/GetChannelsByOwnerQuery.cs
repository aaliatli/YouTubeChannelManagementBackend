using MediatR;

public class GetChannelsByOwnerQuery : IRequest<List<GetAllChannelsQueryResult>>, ICacheable
{
    public Guid OwnerId { get; set; }
    public string CacheKey => "GetAllEvents";
    public int CacheDuration => 10;

}
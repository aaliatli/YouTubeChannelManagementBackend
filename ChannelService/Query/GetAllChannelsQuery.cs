using MediatR;
public class GetAllChannelsQuery : IRequest<List<GetAllChannelsQueryResult>>, ICacheable
{
    public Guid OwnerId { get; set; }
    public string CacheKey => "GetAllEvents";
    public int CacheDuration => 10;
}
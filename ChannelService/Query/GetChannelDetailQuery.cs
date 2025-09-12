using MediatR;

public class GetChannelDetailQuery : IRequest<GetChannelDetailQueryResult>
{
    public string CacheKey => "GetAllEvents";
    public int CacheDuration => 10;
}
public interface ICacheable
{
    string CacheKey { get; }
    int CacheDuration { get; }
}
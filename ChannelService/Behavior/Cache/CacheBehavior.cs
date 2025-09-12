using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class CacheBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheable
{
    private readonly IDistributedCache _cache;

    public CacheBehavior(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheKey = request.CacheKey;
        var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<TResponse>(cachedData)!;
        }

        var response = await next();

        var cacheOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(request.CacheDuration)
        };

        var serialized = JsonSerializer.Serialize(response);
        await _cache.SetStringAsync(cacheKey, serialized, cacheOptions, cancellationToken);

        return response;
    }
}

using System.Net.Http.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class SubscribeToChannelCommandHandler : IRequestHandler<SubscribeToChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    private readonly IHttpClientFactory _httpClient;
    private readonly ChannelDbContext _context;
    private readonly ILogger<SubscribeToChannelCommandHandler> _logger;

    public SubscribeToChannelCommandHandler(IChannelRepository repository, IHttpClientFactory httpClient, ChannelDbContext context, ILogger<SubscribeToChannelCommandHandler> logger)
    {
        _repository = repository;
        _httpClient = httpClient;
        _context = context;
        _logger = logger;
    }
    public async Task<bool> Handle(SubscribeToChannelCommand request, CancellationToken cancellationToken)
    {
        var channelEntity = await _context.Channels
            .AsTracking()
            .FirstOrDefaultAsync(c => c.ChannelId == request.ChannelId, cancellationToken);
        if (channelEntity == null) return false;

        if (channelEntity.OwnerId == request.UserId) return false;

        var client = _httpClient.CreateClient("UserService");
        var response = await client.GetAsync($"api/user/{request.UserId}", cancellationToken);
        if (!response.IsSuccessStatusCode) return false;

        var user = await response.Content.ReadFromJsonAsync<UserInfoDto>(cancellationToken: cancellationToken);
        if (user == null) return false;

        bool alreadySubs = await _context.ChannelSubscribers
            .AnyAsync(x => x.ChannelId == request.ChannelId && x.UserId == request.UserId, cancellationToken);
        if (alreadySubs) return false;
        var userChannel = new ChannelSubscriber { UserId = request.UserId, ChannelId = request.ChannelId };
        var sub = await _context.ChannelSubscribers.AddAsync(userChannel, cancellationToken);
        channelEntity.Subscribers += 1;
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Kullanıcı {UserId}, {ChannelName} kanalına başarıyla abone oldu.", request.UserId, channelEntity.ChannelName);
        return true;
    }

}
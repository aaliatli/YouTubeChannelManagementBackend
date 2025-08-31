using System.Net.Http.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SubscribeToChannelCommandHandler : IRequestHandler<SubscribeToChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    private readonly IHttpClientFactory _httpClient;
    private readonly ChannelDbContext _context;

    public SubscribeToChannelCommandHandler(IChannelRepository repository, IHttpClientFactory httpClient, ChannelDbContext context)
    {
        _repository = repository;
        _httpClient = httpClient;
        _context = context;
    }
    public async Task<bool> Handle(SubscribeToChannelCommand request, CancellationToken cancellationToken)
    {
        var channelEntity = await _context.Channels
            .AsTracking()
            .FirstOrDefaultAsync(c => c.ChannelId == request.ChannelId, cancellationToken);
        if (channelEntity == null) return false;

        if (channelEntity.OwnerId == request.UserId) return false;

        var client = _httpClient.CreateClient("UserService");
        var response = await client.GetAsync($"User/{request.UserId}", cancellationToken);
        if (!response.IsSuccessStatusCode) return false;

        var user = await response.Content.ReadFromJsonAsync<UserInfoDto>(cancellationToken: cancellationToken);
        if (user == null) return false;

        bool alreadySubs = await _context.ChannelSubscribers
            .AnyAsync(x => x.ChannelId == request.ChannelId && x.UserId == request.UserId, cancellationToken);
        if (alreadySubs) return false;

        var userChannel = new ChannelSubscriber { UserId = request.UserId, ChannelId = request.ChannelId };
        await _context.ChannelSubscribers.AddAsync(userChannel, cancellationToken);

        channelEntity.Subscribers += 1;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

}
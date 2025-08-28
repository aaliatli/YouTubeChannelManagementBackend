using System.Net.Http.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SubscribeChannelCommandHandler : IRequestHandler<SubscribeToChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    private readonly IHttpClientFactory _httpClient;
    private readonly ChannelDbContext _context;

    public SubscribeChannelCommandHandler(IChannelRepository repository, IHttpClientFactory httpClient, ChannelDbContext context)
    {
        _repository = repository;
        _httpClient = httpClient;
        _context = context;
    }
    public async Task<bool> Handle(SubscribeToChannelCommand request, CancellationToken cancellationToken)
    {
        var channelEntity = await _repository.GetChannelById(request.ChannelId);
        if (channelEntity == null) return false;

        var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"http://localhost:5200/api/users/{request.UserId}");
        var user = await response.Content.ReadFromJsonAsync<UserInfoDto>();

        if (!response.IsSuccessStatusCode) return false;

        bool alreadySubs = await _context.ChannelSubscribers.AnyAsync(x=> x.ChannelId == request.ChannelId && x.UserId == request.UserId);
        if (alreadySubs) return false;

        var userChannel = new ChannelSubscriber
        {
            UserId = request.UserId,
            ChannelId = request.ChannelId
        };
        _context.ChannelSubscribers.Add(userChannel);
        await _context.SaveChangesAsync();
        return true;
    }
}
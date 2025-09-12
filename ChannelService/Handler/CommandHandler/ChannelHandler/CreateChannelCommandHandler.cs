using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, CreateChannelDto>
{
    private readonly IChannelRepository _repository;
    private readonly ILogger<CreateChannelCommandHandler> _logger;
    private readonly IMemoryCache _cache;
    public CreateChannelCommandHandler(IChannelRepository repository, ILogger<CreateChannelCommandHandler> logger, IMemoryCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }
    public async Task<CreateChannelDto> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        var newChannel = new Channel
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = request.ChannelName,
            Category = request.Category,
            OwnerId = request.OwnerId,
        };
        var createdChannel = await _repository.CreateChannel(newChannel);
        var created = new CreateChannelDto
        {
            ChannelId = createdChannel.ChannelId,
            ChannelName = createdChannel.ChannelName,
            Category = createdChannel.Category,
            OwnerId = createdChannel.OwnerId
        };
        _logger.LogInformation("[LOG] Kanal oluşturuldu: {ChannelId} - {ChannelName}", created.ChannelId ,created.ChannelName);
        _cache.Remove("all_channels");
        return created;
        
    }
}
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

public class UpdateChannelCommandHandler : IRequestHandler<UpdateChannelCommand, Channel>
{
    private readonly IChannelRepository _repository;
    private readonly ILogger<UpdateChannelCommandHandler> _logger;
    public UpdateChannelCommandHandler(IChannelRepository repository, ILogger<UpdateChannelCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<Channel> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
    {
        var update = await _repository.UpdateChannel(new Channel
        {
            ChannelId = request.ChannelId,
            ChannelName = request.ChannelName,
            Category = request.Category
        });
        
        _logger.LogInformation("[LOG] Kanal güncellendi: {ChannelId} - {ChannelName}", request.ChannelId, request.ChannelName);
        
        return update;
    }
}
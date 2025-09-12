using MediatR;
using Microsoft.Extensions.Logging;

public class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    private readonly ILogger<DeleteChannelCommandHandler> _logger;
    public DeleteChannelCommandHandler(IChannelRepository repository, ILogger<DeleteChannelCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<bool> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetChannelById(request.ChannelId);
        if (entity == null)
        {
            _logger.LogInformation("[LOG] Kanal bulunamadı!");
            return false;
        } 
        var channel = await _repository.DeleteChannel(request.ChannelId);
        _logger.LogInformation("[LOG] Kanal silindi: {@Channel}", channel);
        return channel;
    }
}
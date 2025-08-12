using MediatR;

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, Guid>
{
    private readonly IChannelRepository _repository;
    public CreateChannelCommandHandler(IChannelRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        var newChannel = new Channel
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = request.ChannelName,
            Category = request.Category,
        };

        return await _repository.CreateChannel(newChannel);
        
    }
}
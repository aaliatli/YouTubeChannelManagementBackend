using MediatR;

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, CreateChannelDto>
{
    private readonly IChannelRepository _repository;
    public CreateChannelCommandHandler(IChannelRepository repository)
    {
        _repository = repository;
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
        return new CreateChannelDto
        {
            ChannelId = createdChannel.ChannelId,
            ChannelName = createdChannel.ChannelName,
            Category = createdChannel.Category,
            OwnerId = createdChannel.OwnerId
        };
        
    }
}
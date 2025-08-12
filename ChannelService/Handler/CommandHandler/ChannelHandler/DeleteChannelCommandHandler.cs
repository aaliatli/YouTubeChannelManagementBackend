using MediatR;

public class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    public DeleteChannelCommandHandler(IChannelRepository repository) {
        _repository = repository;
    }
    public async Task<bool> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetChannelById(request.ChannelId);
        if (entity == null) return false;
        return await _repository.DeleteChannel(request.ChannelId);
    }
}
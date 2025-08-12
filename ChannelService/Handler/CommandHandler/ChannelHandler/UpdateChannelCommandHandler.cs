using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

public class UpdateChannelCommandHandler : IRequestHandler<UpdateChannelCommand, bool>
{
    private readonly IChannelRepository _repository;
    public UpdateChannelCommandHandler(IChannelRepository repository)
    {
        _repository = repository;
    }   
    public async Task<bool> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
    {
        return await _repository.UpdateChannel(new UpdateChannelDto{
            ChannelId = request.ChannelId,
            ChannelName = request.ChannelName,
            Category = request.Category
        } );
            
           
    }
}
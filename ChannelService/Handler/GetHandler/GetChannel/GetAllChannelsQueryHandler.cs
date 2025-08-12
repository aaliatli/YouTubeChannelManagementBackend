using MediatR;

public class GetAllChannelsQueryHandler : IRequestHandler<GetAllChannelsQuery, IReadOnlyList<ChannelListDto>>
{
    private readonly IChannelRepository _repository;
    public GetAllChannelsQueryHandler(IChannelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ChannelListDto>> Handle(GetAllChannelsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllChannels();
    }
}
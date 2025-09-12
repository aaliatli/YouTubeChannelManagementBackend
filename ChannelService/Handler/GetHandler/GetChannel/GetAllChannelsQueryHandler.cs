using AutoMapper;
using Mapster;
using MediatR;

public class GetAllChannelsQueryHandler : IRequestHandler<GetAllChannelsQuery, List<GetAllChannelsQueryResult>>
{
    private readonly IChannelRepository _repository;
    public GetAllChannelsQueryHandler(IChannelRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllChannelsQueryResult>> Handle(GetAllChannelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllChannels();
        var result = entities.Adapt<List<GetAllChannelsQueryResult>>();
        return result;
    }
}
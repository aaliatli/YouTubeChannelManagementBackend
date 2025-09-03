using AutoMapper;
using MediatR;

public class GetAllChannelsQueryHandler : IRequestHandler<GetAllChannelsQuery, List<GetAllChannelsQueryResult>>
{
    private readonly IChannelRepository _repository;
    private readonly IMapper _mapper;
    public GetAllChannelsQueryHandler(IChannelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllChannelsQueryResult>> Handle(GetAllChannelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllChannels();
        var result = _mapper.Map<List<GetAllChannelsQueryResult>>(entities);
        return result;
    }
}
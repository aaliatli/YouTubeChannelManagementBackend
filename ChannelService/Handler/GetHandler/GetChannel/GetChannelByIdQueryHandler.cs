using AutoMapper;
using MediatR;

public class GetChannelByIdQueryHandler : IRequestHandler<GetChannelByIdQuery, GetChannelByIdQueryResult>
{
    private readonly IChannelRepository _repository;
    private readonly IMapper _mapper;
    public GetChannelByIdQueryHandler(IChannelRepository repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetChannelByIdQueryResult> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetChannelById(request.ChannelId);
        return _mapper.Map<GetChannelByIdQueryResult>(entity);
    }
}

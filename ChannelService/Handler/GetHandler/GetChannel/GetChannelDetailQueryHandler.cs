using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetChannelDetailQueryHandler : IRequestHandler<GetChannelDetailQuery, GetChannelDetailQueryResult>
{
    private readonly IChannelRepository _repository;
    private readonly IMapper _mapper;
    public GetChannelDetailQueryHandler(IChannelRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetChannelDetailQueryResult> Handle(GetChannelDetailQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetChannelDetail();
        var result = _mapper.Map<GetChannelDetailQueryResult>(entities);
        return result;
    }
}
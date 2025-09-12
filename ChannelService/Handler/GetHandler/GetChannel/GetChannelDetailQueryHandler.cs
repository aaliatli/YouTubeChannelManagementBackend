using AutoMapper;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class GetChannelDetailQueryHandler : IRequestHandler<GetChannelDetailQuery, GetChannelDetailQueryResult>
{
    private readonly IChannelRepository _repository;

    public GetChannelDetailQueryHandler(IChannelRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetChannelDetailQueryResult> Handle(GetChannelDetailQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetChannelDetail();
        var result = entities.Adapt<GetChannelDetailQueryResult>();
        return result;
    }
}
using AutoMapper;
using Mapster;
using MediatR;

public class GetChannelByIdQueryHandler : IRequestHandler<GetChannelByIdQuery, GetChannelByIdQueryResult>
{
    private readonly IChannelRepository _repository;

    public GetChannelByIdQueryHandler(IChannelRepository repository) {
        _repository = repository;
    }
    public async Task<GetChannelByIdQueryResult> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetChannelById(request.ChannelId);
        if (entity == null)
        {
            return null;
        }
        return entity.Adapt<GetChannelByIdQueryResult>();
    }
}

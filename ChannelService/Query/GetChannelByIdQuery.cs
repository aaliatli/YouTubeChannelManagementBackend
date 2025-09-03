using MediatR;
public class GetChannelByIdQuery : IRequest<GetChannelByIdQueryResult>
{
    public Guid ChannelId { get; set; }
}
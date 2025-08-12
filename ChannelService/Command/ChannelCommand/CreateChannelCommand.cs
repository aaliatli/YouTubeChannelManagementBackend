using MediatR;

public class CreateChannelCommand : IRequest<Guid>
{
    public string ChannelName { get; set; }
    public string Category { get; set; }

}
using MediatR;

public class SubscribeToChannelCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid ChannelId { get; set; }
    
}
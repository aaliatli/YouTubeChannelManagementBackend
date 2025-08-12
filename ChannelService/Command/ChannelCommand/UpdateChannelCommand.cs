using MediatR;

public class UpdateChannelCommand : IRequest<bool>
{
    public Guid ChannelId{ get; set; }
    public string ChannelName { get; set; }
    public string Category{ get; set; }
}
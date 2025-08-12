using MediatR;

public class DeleteChannelCommand : IRequest<bool>
{
    public Guid Id;
    public Guid ChannelId;
}
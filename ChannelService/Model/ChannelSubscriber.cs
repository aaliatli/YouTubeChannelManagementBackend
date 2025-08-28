public class ChannelSubscriber
{
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    public Channel Channel { get; set; }
}
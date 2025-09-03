public class ChannelSubscriber
{
    public Guid ChannelId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    public Channel Channel { get; set; }
}
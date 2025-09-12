public class Channel
{
    public Guid ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string Category { get; set; }
    public long Subscribers { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid OwnerId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public ICollection<ChannelSubscriber> ChannelSubscribers { get; set; } = new List<ChannelSubscriber>();


}
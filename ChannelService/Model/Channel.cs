public class Channel
{
    public Guid ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string Category { get; set; }
    public long Subscribers { get; set; }
    public bool IsActive { get; set; } = false;
    public DateTime CreationDate { get; set; }
    public ICollection<ChannelSubscriber> ChannelSubscribers { get; set; } = new List<ChannelSubscriber>();
}
public class CreateChannelDto
{
    public Guid ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string Category { get; set; }
    public long Subscribers { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreationDate { get; set; }
}
public class CreateChannelDto
{
    public string ChannelName { get; set; }
    public string Category { get; set; }

    public Guid ChannelId{ get; set; }
    public Guid OwnerId { get; set; }
}
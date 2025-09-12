public class GetAllChannelsQueryResult
{
    public Guid ChannelId { get; set; }
    public Guid OwnerId { get; set; }
    public string ChannelName { get; set; }
    public long Subscribers { get; set; }
    public string Category { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreationDate { get; set; }
    
}
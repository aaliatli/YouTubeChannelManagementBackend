using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class UpdateChannelCommand : IRequest<Channel>
{
    [JsonIgnore]
    public Guid ChannelId { get; set; }
    public string ChannelName { get; set; }
    public string Category{ get; set; }
}
using System.Text.Json.Serialization;
using MediatR;

public class CreateChannelCommand : IRequest<CreateChannelDto>
{

    public string ChannelName { get; set; }
    public string Category { get; set; }
    [JsonIgnore]
    public Guid OwnerId { get; set; }

}
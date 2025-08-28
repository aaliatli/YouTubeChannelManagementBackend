using System.Net.Http.Json;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly IChannelRepository _repository;
    private readonly IMediator _mediator;
    private readonly IHttpClientFactory _httpClient;
    public ChannelController(IChannelRepository repository, IMediator mediator, IHttpClientFactory httpClient)
    {
        _repository = repository;
        _mediator = mediator;
        _httpClient = httpClient;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateChannel(CreateChannelCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpGet("get-channel")]
    public async Task<IActionResult> GetChannel()
    {
        return Ok(await _mediator.Send(new GetAllChannelsQuery()));
    }
    [HttpDelete("{channelId}")]
    public async Task<IActionResult> DeleteChannel(Guid channelId)
    {
        var command = new DeleteChannelCommand
        {
            ChannelId = channelId,
        };
        return Ok(await _mediator.Send(command));
    }
    [HttpPut("update")]
    public async Task<IActionResult> UpdateChannel([FromBody] UpdateChannelDto dto)
    {
        var entity = await _mediator.Send(new UpdateChannelCommand
        {
            ChannelId = dto.ChannelId,
            ChannelName = dto.ChannelName,
            Category = dto.Category
        });
        return Ok(entity);
    }
    [HttpPost("{channelId}/subscribe")]
    public async Task<IActionResult> Subscribe(Guid channelId, Guid userId)
    {
        var sub = await _mediator.Send(new SubscribeToChannelCommand
        {
            ChannelId = channelId,
            UserId = userId
        });
        return sub ? Ok("Abone olundu.") : BadRequest("Abone olunamadı.");
    }

    [HttpPost("subscribe-to-channel")]
    public async Task<IActionResult> SubscribeToChannel(SubscribeToChannelCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
}
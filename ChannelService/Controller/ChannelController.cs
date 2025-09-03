using System.Net.Http.Json;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? User.FindFirstValue("userId");

        if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var ownerId))
            return Unauthorized("Token içinde geçerli kullanıcı kimliği bulunamadı.");

        command.OwnerId = ownerId;

        var result = await _mediator.Send(command);
        return Created(string.Empty, result);
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

    [Authorize] 
    [HttpPost("{channelId:guid}/subscribe")]
    public async Task<IActionResult> SubscribeToChannel(Guid channelId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _httpClient.CreateClient("UserService").GetAsync($"User/{userId}");

        if (!response.IsSuccessStatusCode) return NotFound("Kullanıcı bulunamadı.");
        var result = await _mediator.Send(new SubscribeToChannelCommand
        {
            ChannelId = channelId,
            UserId = userId,
        });
        return Ok(result);
    }
    
}
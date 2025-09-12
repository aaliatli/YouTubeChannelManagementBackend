using System.Net.Http.Json;
using System.Security.Claims;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.CustomUI;
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
    [HttpPost]
    public async Task<IActionResult> Create(CreateChannelCommand command)
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

    [Authorize]
    [HttpPost("{channelId:guid}/subscribe")]
    public async Task<IActionResult> Subscribe(Guid channelId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _httpClient.CreateClient("UserService").GetAsync($"api/User/{userId}");

        if (!response.IsSuccessStatusCode) return NotFound("Kullanıcı bulunamadı.");
        var result = await _mediator.Send(new SubscribeToChannelCommand
        {
            ChannelId = channelId,
            UserId = userId,
        });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery]GetChannelsPagedQuery query)
    {
        var paged = await _mediator.Send(query);
        return Ok(paged);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllChannelsQuery()));
    }
    
    [Authorize]
    [HttpGet("mine")]
    public async Task<IActionResult> GetMine()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdStr, out var userId))
            return Unauthorized("Geçersiz kullanıcı kimliği");

        var query = new GetChannelsByOwnerQuery { OwnerId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{channelId:guid}")]
    public async Task<IActionResult> GetById(Guid channelId)
    {
        var result = await _mediator.Send(new GetChannelByIdQuery { ChannelId = channelId });

        if (result == null)
            return NotFound($"Kanal bulunamadı: {channelId}");

        return Ok(result);
    }
    
    [HttpDelete("{channelId:guid}")]
    public async Task<IActionResult> Delete(Guid channelId)
    {
        var command = new DeleteChannelCommand
        {
            ChannelId = channelId,
        };
        return Ok(await _mediator.Send(command));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChannelCommand command)
    {
        var cmd = new UpdateChannelCommand
        {
            ChannelId = id,
            ChannelName = command.ChannelName,
            Category = command.Category
        };
        
        var entity = await _mediator.Send(cmd);
        
        return Ok(entity);
    }

}
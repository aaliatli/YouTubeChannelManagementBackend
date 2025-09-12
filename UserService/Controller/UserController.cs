using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("Kullanıcı kaydedildi.");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
    {
        var loginUser = await _mediator.Send(command);
        if(string.IsNullOrEmpty(loginUser.Token)){ return Unauthorized("Kullanıcı ya da şifre hatalı!"); }
        return Ok(new { loginUser });
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserByIdQuery { UserId = id }));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserByIdCommand { UserId = id };
        var result = await _mediator.Send(command);
        if (!result) return NotFound("Silinecek kanal bulunamadı.");
        return Ok("Kanal silindi");
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
    {
        command.UserId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
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

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("Kullanıcı kaydedildi.");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        if(string.IsNullOrEmpty(token)){ return Unauthorized("Kullanıcı ya da şifre hatalı!"); }
        return Ok(new { token });
    }
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserByIdQuery { UserId = id }));
    }
    [HttpDelete("user-delete")]
    public async Task<IActionResult> DeleteUser(DeleteUserByIdCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
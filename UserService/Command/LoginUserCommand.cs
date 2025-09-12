using MediatR;

public class LoginUserCommand : IRequest<LoginUserDto>
{
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
}
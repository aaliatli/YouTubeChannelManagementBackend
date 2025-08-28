using MediatR;

public class LoginUserCommand : IRequest<string>
{
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
}
using MediatR;

public class LoginUserCommand : IRequest<User>
{
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
}
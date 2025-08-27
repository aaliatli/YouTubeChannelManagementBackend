using MediatR;

public class UpdateUserCommand : IRequest<User>
{
    public Guid UserId{ get; set; }
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public string UserMail { get; set; }
    public string UserPassword { get; set; }
}
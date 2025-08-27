using MediatR;

public class GetUserByIdQuery : IRequest<User>
{
    public Guid UserId{ get; set; }
}
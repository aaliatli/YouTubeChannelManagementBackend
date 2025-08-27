using MediatR;

public class DeleteUserByIdCommand : IRequest<bool>
{
    public Guid UserId{ get; set; }
}
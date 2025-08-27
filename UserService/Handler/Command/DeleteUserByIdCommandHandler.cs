using MediatR;

public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
{
    private readonly IUserRepository _repository;

    public DeleteUserByIdCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetUserById(request.UserId);
        if (existingUser == null || existingUser.IsDeleted)
        {
            return false;
        }

        existingUser.IsDeleted = true;
        await _repository.UpdateUser(existingUser);
        return true;
    }
}
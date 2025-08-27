using MediatR;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUserRepository _repository;

    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetUserById(request.UserId);
        existingUser.UserName = request.UserName;
        existingUser.UserLastName = request.UserLastName;
        existingUser.UserMail = request.UserMail;
        existingUser.UserPassword = request.UserPassword;
        await _repository.UpdateUser(existingUser);
        return existingUser;
    }

}

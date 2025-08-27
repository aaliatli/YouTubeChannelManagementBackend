using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, User>
{
    private readonly IUserRepository _repository;

    public LoginUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.LoginUser(request.UserMail, request.UserPassword);
        return existingUser;
    }

}
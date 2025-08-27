using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            UserLastName = request.UserLastName,
            UserMail = request.UserMail,
            UserPassword = request.UserPassword
        };
        await _repository.RegisterUser(newUser);
        return newUser;
    }
}

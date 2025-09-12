using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserRequestDto>
{
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<RegisterUserRequestDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            UserLastName = request.UserLastName,
            UserMail = request.UserMail,
            UserPassword = request.UserPassword
        };
        var created = await _repository.RegisterUser(newUser);
        return new RegisterUserRequestDto
        {
            UserId = Guid.NewGuid(),
            UserName = created.UserName,
            UserLastName = created.UserLastName,
            UserMail = created.UserMail,
            UserPassword = created.UserPassword
        };;
    }
}

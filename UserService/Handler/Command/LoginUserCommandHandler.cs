using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    public LoginUserCommandHandler(IUserRepository repository, JwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.LoginUser(request.UserMail, request.UserPassword);
        
        if (existingUser == null) 
        { 
            return null; 
        }

        var token = _jwtService.GenerateToken(existingUser.UserId, existingUser.UserMail, "User");
        
        return new LoginUserDto
        {
            UserMail = existingUser.UserMail,
            UserPassword = existingUser.UserPassword,
            Token = token,
        };

    }
}
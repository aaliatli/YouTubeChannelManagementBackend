using MediatR;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _repository;
    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserById(request.UserId);
        return user;
    }

}
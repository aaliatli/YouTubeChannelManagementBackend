public interface IUserRepository
{
    Task<User> RegisterUser(User user);
    Task<User> LoginUser(string userMail, string password);
    Task<bool> DeleteUserById(Guid id);
    Task<User> GetUserById(Guid id);
    Task<List<User>> GetAllUsers();
    Task UpdateUser(User entity);
}
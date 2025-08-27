
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    public UserRepository(UserDbContext context) {
        _context = context;
    }
    public async Task<bool> DeleteUserById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> LoginUser(string userMail, string userPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserMail == userMail && u.UserPassword == userPassword);
        return user;
    }

    public async Task<User> RegisterUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

}
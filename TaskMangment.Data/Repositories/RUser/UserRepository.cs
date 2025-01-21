using Microsoft.EntityFrameworkCore;
using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RUser;

public class UserRepository : IUserRepository
{
    private readonly ToDoListDbContext _context;

    public UserRepository(ToDoListDbContext context)
    {
        _context = context;
    }

    public Task<UserEntity?> AuthorizeUserAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<bool> UpdateUserAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(string username)
    {
        throw new NotImplementedException();
    }
}
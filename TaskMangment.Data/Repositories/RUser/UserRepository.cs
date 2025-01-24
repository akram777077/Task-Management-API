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

    public async Task<bool> UpdateUserAsync(UserEntity user)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.UserName== user.UserName);
        if (result is null)
            return false;
        _context.Entry(result)
        .CurrentValues
        .SetValues(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(string username)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (result is null)
            return false;
        _context.Users.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }
}
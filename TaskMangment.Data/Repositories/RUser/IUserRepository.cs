using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RUser;

public interface IUserRepository
{
    public Task<AuthorizeUserEntity?> AuthorizeUserAsync(LoginEntity user);
    public Task<bool> CreateUserAsync(UserEntity user);
    public Task<bool> UpdateUserAsync(UserEntity user);
    public Task<bool> DeleteUserAsync(string username);
}
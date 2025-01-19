using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RUser;

public interface IUserRepository
{
    public Task<UserEntity?> AuthorizeUserAsync(UserEntity user);
    public Task<UserEntity>  CreateUserAsync(UserEntity user);
    public Task<bool> UpdateUserAsync(UserEntity user);
    public Task<bool> DeleteUserAsync(string username);
}
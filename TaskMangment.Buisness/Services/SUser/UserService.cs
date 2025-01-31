using TaskMangment.Buisness.Mapping;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Models.Users;
using TaskMangment.Data.Repositories.RUser;

namespace TaskMangment.Buisness.Services.SUser;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthorizeUserModel?> AuthorizeAsync(LoginModel user)
    {
        var entity = user.ToEntity();
        var result = await _repository.AuthorizeUserAsync(entity);
        return result?.ToModel();
    }

    public async Task<bool> CreateAsync(UserModel user)
    {
        var entity = user.ToEntity();
        return await _repository.CreateUserAsync(entity);
    }

    public async Task<bool> DeleteAsync(string username)
    {
        return await _repository.DeleteUserAsync(username);
    }

    public async Task<bool> UpdateAsync(UserModel user)
    {
        var entity = user.ToEntity();
        var result =  await _repository.UpdateUserAsync(entity);
        return result;
    }
}
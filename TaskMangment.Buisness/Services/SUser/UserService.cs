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

    public Task<AuthorizeUserModel> CreateAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(UserModel user)
    {
        throw new NotImplementedException();
    }
}
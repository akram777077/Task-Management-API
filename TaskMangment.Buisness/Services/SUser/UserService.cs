using TaskMangment.Buisness.Mapping;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Models.Users;
using TaskMangment.Data.Repositories.RUser;

namespace TaskMangment.Buisness.Services.SUser;

public class UserService : IUserService
{
    private readonly UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    public Task<AuthorizeUserModel?> AuthorizeAsync(LoginModel user)
    {
        throw new NotImplementedException();
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
using System;
using TaskMangment.Buisness.Models.Users;
namespace TaskMangment.Buisness.Services.SUser;

public interface IUserService
{
    public Task<AuthorizeUserModel?> AuthorizeAsync(LoginModel user);
    public Task<AuthorizeUserModel> CreateAsync(UserModel user);
    public Task<bool> UpdateAsync(UserModel user);
    public Task<bool> DeleteAsync(string username);
}

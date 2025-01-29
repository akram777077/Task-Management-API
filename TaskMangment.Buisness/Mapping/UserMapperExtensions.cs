using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Models.Users;
using TaskMangment.Data.Entities;

namespace TaskMangment.Buisness.Mapping;

public static class UserMapperExtensions
{
    public static UserEntity ToEntity(this UserModel model)
    {
        return new UserEntity
        {
            Password = model.Password,
            UserName = model.Username,
            RoleName = model.Role
        };
    }
    public static AuthorizeUserModel ToModel(this AuthorizeUserEntity entity)
    {
        return new AuthorizeUserModel
        (
            entity.Username, 
            entity.Role
        );
    }
    public static LoginEntity ToEntity(this LoginModel model)
    {
        return new LoginEntity
        {
            Password = model.Password,
            Username = model.Username,
        };
    }
}
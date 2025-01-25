using System;
using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Mapping;

public static class UserMapperExtensions
{
    public static AuthorizeUserEntity ToAuthorizeUser(this UserEntity login)
    {
        return new ()
        {
            Role = login.RoleName,
            Username = login.UserName
        };
    }
}

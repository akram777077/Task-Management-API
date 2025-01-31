using System;
using AutoMapper;
using TaskMangment.Api.DTOs;
using TaskMangment.Buisness.Models.Users;

namespace TaskMangment.Api.Mapping;

public class UserMangmentProfile : Profile
{
    public UserMangmentProfile()
    {
        CreateMap<NewUserWithoutRoleDto, UserModel>()
        .ConstructUsing((x, context) => 
            new UserModel(
                x.Username, 
                x.Password,
                context.Items["role"] as string ?? string.Empty
            )
        );
    }
}

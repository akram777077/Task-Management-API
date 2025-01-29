using System;
using AutoMapper;
using TaskMangment.Api.DTOs;
using TaskMangment.Buisness.Models.Users;

namespace TaskMangment.Api.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
            CreateMap<LoginDto,LoginModel>()
            .ConstructUsing((x, context) => 
                new LoginModel(
                    x.username, 
                    x.password
                )
            );
    }
}

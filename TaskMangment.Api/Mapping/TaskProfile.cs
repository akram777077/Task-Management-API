using System;
using AutoMapper;
using TaskMangment.Api.DTOs;
using TaskMangment.Buisness.Models;

namespace TaskMangment.Api.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskModel,ResponseDto>();
    }
}
